using GPTArticleGen.Model;
using GPTArticleGen.View;
using Microsoft.Web.WebView2.WinForms;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GPTArticleGen.Presenter
{
    internal class ArticlePresenter
    {
        private readonly IArticleView _view;
        private readonly ArticleModel _model;
        private string _basicPrompt;
        private WordpressRepository _wordpressRepository;
        private PageModel _pageModel;
        private SQLiteDB _db;

        #region Initialize
        public ArticlePresenter(IArticleView view, ArticleModel model)
        {
            _view = view;
            _model = model;
            _wordpressRepository = new WordpressRepository();   

            _view.GenerateArticle += GenerateArticle;
            _view.GenerateForAll += GenerateForAll;
            _view.ChangeDefaultPrompt += ChangeDefaultPrompt;
            _view.ImportTitles += ImportTitles;
            _view.AddToPageAsync += AddToPageAsync;
            _view.RegenarateArticle += RegenarateArticle;
            _view.SelectedTitleChanged += SelectedTitleChanged;
            _view.PromptFormatTextBoxChanged += PromptFormatTextBoxChanged;
            _view.PromptTextBoxChanged += PromptTextBoxChanged;
            _view.TitleTextBoxChanged += TitleTextBoxChanged;
            _view.ContentTextBoxChanged += ContentTextBoxChanged;
            _view.TagsTextBoxChanged += TagsTextBoxChanged;
            _view.SaveSettings += SaveSettings;
            _view.CancelSettings += CancelSettings;
        }

        public void Initialize()
        {
            _view.Title = _model.Title;
            _view.Content = _model.Content;
            //_view.Tags = _model.Tags;
            _view.Prompt = _model.Prompt;
            // _view.Description = _model.Description;
            _view.WebView2.Source = new Uri("https://chat.openai.com");
            //_view.Tags = new ObservableCollection<string>();

            // Initialize SQLiteDB
            _db = new SQLiteDB();
            _db.OpenConnection();

            // Create table of Pages if not exists
            string createPagesTableQuery = @"CREATE TABLE IF NOT EXISTS Pages (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                site TEXT,
                username TEXT,
                password TEXT
            )";
            SQLiteCommand createPagesTableCommand = _db.CreateCommand();
            createPagesTableCommand.CommandText = createPagesTableQuery;
            createPagesTableCommand.ExecuteNonQuery();

            // Create table if not exists
            string createTableQuery = @"CREATE TABLE IF NOT EXISTS Articles (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                title TEXT,
                content TEXT,
                tags TEXT,
                prompt TEXT,
                isPublished BOOLEAN,
                page_id INTEGER REFERENCES Pages(id)
            )";
            SQLiteCommand createTableCommand = _db.CreateCommand();
            createTableCommand.CommandText = createTableQuery;
            createTableCommand.ExecuteNonQuery();

            // Close connection to database
            _db.CloseConnection();

            _basicPrompt = Properties.Settings.Default.BasicPrompt;
            _view.DefaultPrompt = _basicPrompt;
            _view.MaxRetries = Properties.Settings.Default.MaxRetries;
        }
        #endregion

        #region Event Handlers
        private async void AddToPageAsync(object? sender, EventArgs e)
        {
            // Initialize SQLiteDB
            SQLiteDB db = new SQLiteDB();
            db.OpenConnection();

            foreach (ArticleModel article in _view.Titles)
            {
                // Create an object to hold your article data
                var articleData = new
                {
                    title = article.Title,
                    content = article.Content,
                    status = "publish",
                    tags = new[] { "[tag]" }
                };

                // Serialize the object to JSON
                article.PostData = JsonConvert.SerializeObject(articleData);

                article.PostData = article.PostData.Replace("\"[tag]\"", "[tag]");

                List<string> tags = new List<string>();

                // Find page by ID
                PageModel page = await db.GetPageById(article.SiteId);

                // Get tags
                if (!String.IsNullOrEmpty(article.Tags))
                    tags = article.Tags.Split(", ").ToList();

                //Add to wordpress
                if(await _wordpressRepository.AddPostAsync(tags, article.PostData, page.Username, page.Password, page.Site, ""))
                {
                    //Add to database
                    await db.InsertArticleAsync(article);
                }
                else
                {
                    Debug.WriteLine("Failed to add post to wordpress. Post title: " + article.Title );
                }

                await db.InsertArticleAsync(article);
            }

            db.CloseConnection();
        }

        private async void ImportTitles(object? sender, EventArgs e)
        {
            // Create and configure the OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select a CSV file",
                Filter = "CSV Files (*.csv)|*.csv",
                CheckFileExists = true
            };

            // Show the dialog and get the selected file path
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;

                try
                {
                    // Call the LoadDataAsync function with the selected file path
                    await LoadDataAsync(selectedFilePath);
                    _view.SelectedTitle = _view.Titles.FirstOrDefault();
                    SelectedTitleChanged(this, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that may occur during data loading
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ChangeDefaultPrompt(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void GenerateForAll(object? sender, EventArgs e)
        {
            bool isFirst = true;
            Task.Run(async () =>
            {
                // Update the UI with the result on the UI thread
                Program.SyncContext.Post(async _ =>
                {
                    //_view.DisableUI();

                    foreach (ArticleModel selected in _view.Titles)
                    {
                        if (isFirst)
                        {
                            await GenerateByGPTAsync(_view.WebView2, _view, selected.Prompt, selected);
                            isFirst = false;
                        }
                        else
                            await EditRegenerateByGPTAsync(_view.WebView2, _view, selected.Prompt, selected);

                        selected.Title = await ExtractValueBetweenAsync(selected.RawData, "Meta title:", "Meta content:");
                        selected.Content = await ExtractValueBetweenAsync(selected.RawData , "Meta content:", "Meta tags:");
                        selected.Tags = await ExtractTagsAsync(selected.RawData, "Meta tags:");

                        selected.Tags = SubstreingFromString(selected.Tags, selected.Retries);
                        selected.Retries++;

                        SelectedTitleChanged(this, EventArgs.Empty);

                        await Task.Delay(TimeSpan.FromSeconds(2));
                    }
                    //_view.EnableUI();
                }, null);
            });
        }

        private void RegenarateArticle(object? sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                // Update the UI with the result on the UI thread
                Program.SyncContext.Post(async _ =>
                {
                    //_view.DisableUI();

                    ArticleModel selected = _view.Titles.FirstOrDefault(item => item == _view.SelectedTitle);
                    await RegenarateByGPTAsync(_view.WebView2, _view, selected);

                    selected.Title = await ExtractValueBetweenAsync(selected.RawData, "Meta title:", "Meta content:");
                    selected.Content = await ExtractValueBetweenAsync(selected.RawData, "Meta content:", "Meta tags:");
                    selected.Tags = await ExtractTagsAsync(selected.RawData, "Meta tags:");

                    selected.Tags = SubstreingFromString(selected.Tags, selected.Retries);
                    selected.Retries++;

                    SelectedTitleChanged(this, EventArgs.Empty);

                    //_view.EnableUI();

                }, null);
            });
        }

        private void GenerateArticle(object? sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                // Update the UI with the result on the UI thread
                Program.SyncContext.Post(async _ =>
                {
                    //_view.DisableUI();

                    ArticleModel selected = _view.Titles.FirstOrDefault(item => item == _view.SelectedTitle);
                    await GenerateByGPTAsync(_view.WebView2, _view, selected.Prompt, selected);

                    selected.Title = await ExtractValueBetweenAsync(selected.RawData, "Meta title:", "Meta content:");
                    selected.Content = await ExtractValueBetweenAsync(selected.RawData, "Meta content:", "Meta tags:");
                    selected.Tags = await ExtractTagsAsync(selected.RawData, "Meta tags:");

                    selected.Tags = SubstreingFromString(selected.Tags, selected.Retries);
                    selected.Retries++;

                    SelectedTitleChanged(this, EventArgs.Empty);

                    //_view.EnableUI();
                }, null);
            });
        }

        private void SelectedTitleChanged(object? sender, EventArgs e)
        {
            ArticleModel selectedArticle = _view.Titles.FirstOrDefault(item => item == _view.SelectedTitle);

            // Update your view accordingly (e.g., set the Title and Description properties)
            if (selectedArticle != null)
            {
                if(String.IsNullOrEmpty(selectedArticle.Title))
                    _view.Title = selectedArticle.PromptTitle;
                else
                    _view.Title = selectedArticle.Title;
                _view.Content = selectedArticle.Content;
                _view.Tags = selectedArticle.Tags;
                _view.PromptFormat = selectedArticle.PromptFormat;
                _view.Prompt = selectedArticle.Prompt;
                

            }
        }

        private void PromptFormatTextBoxChanged(object? sender, EventArgs e)
        {
            ArticleModel article = _view.Titles.FirstOrDefault(item => item == _view.SelectedTitle);

            if (article != null)
            {
                article.PromptFormat = _view.PromptFormat;
                article.Prompt = _view.PromptFormat.Replace("{title}", article.PromptTitle);
            }
        }

        private void PromptTextBoxChanged(object? sender, EventArgs e)
        {
            ArticleModel selectedArticle = _view.Titles.FirstOrDefault(item => item == _view.SelectedTitle);
            
            if (selectedArticle != null)
                selectedArticle.Prompt = _view.Prompt;


        }

        private void TagsTextBoxChanged(object? sender, EventArgs e)
        {
            ArticleModel selectedArticle = _view.Titles.FirstOrDefault(item => item == _view.SelectedTitle);

            if (selectedArticle != null)
                selectedArticle.Tags = _view.Tags;
        }

        private void ContentTextBoxChanged(object? sender, EventArgs e)
        {
            ArticleModel selectedArticle = _view.Titles.FirstOrDefault(item => item == _view.SelectedTitle);

            if (selectedArticle != null)
                selectedArticle.Content = _view.Content;
        }

        private void TitleTextBoxChanged(object? sender, EventArgs e)
        {
            ArticleModel selectedArticle = _view.Titles.FirstOrDefault(item => item == _view.SelectedTitle);

            if (selectedArticle != null)
                selectedArticle.Title = _view.Title;
        }

        private void CancelSettings(object? sender, EventArgs e)
        {
            _view.DefaultPrompt = Properties.Settings.Default.BasicPrompt;
            _view.MaxRetries = Properties.Settings.Default.MaxRetries;
        }

        private void SaveSettings(object? sender, EventArgs e)
        {
            // Assign your new values
            Properties.Settings.Default.BasicPrompt = _view.DefaultPrompt;
            Properties.Settings.Default.MaxRetries = _view.MaxRetries;

            // Save the changes
            Properties.Settings.Default.Save();
        }
        #endregion

        #region Webview2 Methods
        static async Task<string> ExecuteJavaScriptAndWaitAsync(WebView2 webView2, string jsCode, string targetAttribute)
        {
            if (webView2 != null)
            {
                try
                {
                    // Execute the JavaScript code
                    await webView2.ExecuteScriptAsync(jsCode);
                    await Task.Delay(TimeSpan.FromSeconds(5));
                    string jsFirstCheck = @"
                        let button = document.querySelector('button.btn.relative.-z-0.whitespace-nowrap.border-0.md\\:border');
                    ";
                    string jsCheck = @"
                        button = document.querySelector('button.btn.relative.-z-0.whitespace-nowrap.border-0.md\\:border'); 
                        if (button) {
                            true;
                        }
                        else {
                            false;
                        }";

                    await webView2.ExecuteScriptAsync(jsFirstCheck);

                    bool resultCheck = false;
                    string resultLocal;
                    // Wait for the element with the specified attribute
                    while (!resultCheck)
                    {
                        await Task.Delay(TimeSpan.FromSeconds(0.5));
                        resultLocal = await webView2.ExecuteScriptAsync(jsCheck);
                        resultCheck = bool.Parse(resultLocal);
                    }

                    string highestNValue = await FindHighestNAsync(webView2);

                    // Wait for the element with the specified attribute
                    string result = await WaitForElementWithAttributeAsync(webView2, targetAttribute, highestNValue);
                    
                    webView2.Reload();
                    return result;
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    Debug.WriteLine(ex);
                }
            }

            return null;
        }

        static async Task<string> WaitForElementWithAttributeAsync(WebView2 webView2, string targetAttribute, string highestNValue)
        {
            // You can adjust the maximum number of retries and the delay between retries as needed.
            int maxRetries = 10;
            int currentRetry = 0;
            string result = null;

            while (currentRetry < maxRetries)
            {
                result = await webView2.ExecuteScriptAsync(
                    $@"document.querySelector('[data-testid^=""{highestNValue}""]')?.textContent"
                );

                if (!string.IsNullOrEmpty(result))
                {
                    return System.Text.RegularExpressions.Regex.Unescape(result);
                }

                // Wait for a short period before retrying
                await Task.Delay(1000); // Adjust the delay as needed
                currentRetry++;
            }

            Console.WriteLine("Element not found after retries.");

            return null;
        }

        static async Task<string> FindHighestNAsync(WebView2 webView2)
        {
            string jsCode = @"
                let elements = document.querySelectorAll('[data-testid^=""conversation-turn-""]');
                let highestN = -1;
                elements.forEach(element => {
                    let value = parseInt(element.getAttribute('data-testid').substring('conversation-turn-'.length));
                    if (!isNaN(value) && value > highestN) {
                        highestN = value;
                    }
                });
                highestN;
            ";

            string result = await webView2.ExecuteScriptAsync(jsCode);

            if (!string.IsNullOrEmpty(result) && int.TryParse(result, out int highestNValue))
            {
                return $"conversation-turn-{highestNValue}";
            }

            Console.WriteLine("No matching element found.");
            return null;
        }

        static async Task GenerateByGPTAsync(WebView2 webView2, IArticleView view, string newTitle, ArticleModel article)
        {
           

            string jsCode = @"
                let inputElement = document.getElementById('prompt-textarea');
                inputElement.focus();
                document.execCommand('insertText', false, '{placeholder}');
                document.querySelector('[data-testid=""send-button""]').disabled = false;
                document.querySelector('[data-testid=""send-button""]').click();
                document.querySelector('[data-testid=""send-button""]').disabled = false;
            ";

            jsCode = jsCode.Replace("{placeholder}", newTitle);

            string targetAttribute = "conversation-turn-"; // You can set the target attribute here

            string result = await ExecuteJavaScriptAndWaitAsync(webView2, jsCode, targetAttribute);

            if (!string.IsNullOrEmpty(result))
            {
                article.RawData = result;
            }
            else
            {
                Console.WriteLine("No matching element found.");
            }
        }

        static async Task RegenarateByGPTAsync(WebView2 webView2, IArticleView view, ArticleModel article)
        {
            string jsCode = @"
                let customButton = document.querySelector('button.btn.relative.-z-0.whitespace-nowrap.border-0.md\\:border');
                if (customButton) {
                    customButton.click();
                }
            ";

            string targetAttribute = "conversation-turn-"; // You can set the target attribute here

            string result = await ExecuteJavaScriptAndWaitAsync(webView2, jsCode, targetAttribute);

            if (!string.IsNullOrEmpty(result))
            {
                article.RawData = result;
            }
            else
            {
                Console.WriteLine("No matching element found.");
            }
        }

        static async Task EditRegenerateByGPTAsync(WebView2 webView2, IArticleView view, string newTitle, ArticleModel article)
        {
            string jsCode = @"
                (async()=>{
                    document.querySelector('.p-1.gizmo\\:pl-0.rounded-md.disabled\\:dark\\:hover\\:text-gray-400.dark\\:hover\\:text-gray-200.dark\\:text-gray-400.hover\\:bg-gray-100.hover\\:text-gray-700.dark\\:hover\\:bg-gray-700').click();
                    await new Promise(r => setTimeout(r, 1000));    
                    let inputElement = document.querySelector('.m-0.resize-none.border-0.bg-transparent.p-0.focus\\:ring-0.focus-visible\\:ring-0')
                    inputElement.value = '';
                    inputElement.focus();
                    document.execCommand('insertText', false, '{placeholder}');
                    document.querySelector('.btn.relative.btn-primary.mr-2').click();
                })();
            ";

            jsCode = jsCode.Replace("{placeholder}", newTitle);

            string targetAttribute = "conversation-turn-"; // You can set the target attribute here

            string result = await ExecuteJavaScriptAndWaitAsync(webView2, jsCode, targetAttribute);
            await Task.Delay(TimeSpan.FromSeconds(5));

            if (!string.IsNullOrEmpty(result))
            {
                article.RawData = result;
            }
            else
            {
                Console.WriteLine("No matching element found.");
            }
        }
        #endregion

        #region String Operations Methods
        static async Task<string> ExtractValueBetweenAsync(string text, string startMarker, string endMarker)
        {
            int startIndex = text.IndexOf(startMarker);
            if (startIndex >= 0)
            {
                startIndex += startMarker.Length; // Move past the startMarker
                int endIndex = text.IndexOf(endMarker, startIndex);
                if (endIndex >= 0)
                {
                    return text.Substring(startIndex, endIndex - startIndex).Trim();
                }
                // If endMarker is not found, return from startIndex to the end of the text
                return text.Substring(startIndex).Trim();
            }
            return null; // Handle error if startMarker is not found
        }

        static async Task<string> ExtractTagsAsync(string text, string startMarker)
        {
            int startIndex = text.IndexOf(startMarker);
            if (startIndex >= 0)
            {
                startIndex += startMarker.Length;
                string tagSection = text.Substring(startIndex).Trim();
                string[] tags = tagSection.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < tags.Length; i++)
                {
                    tags[i] = tags[i].Trim();
                }
                return string.Join(", ", tags);
            }
            return null; // Return null if marker is not found
        }

        public static string SubstreingFromString(string input, int retries)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            if (retries <= 0)
            {
                return input;
            }

            // Remove the last character
            input = input.Substring(0, input.Length - 1);

            if (retries > 1)
            {
                // Find the last "/" character and remove everything after it
                int lastIndex = input.LastIndexOf('/');
                if (lastIndex >= 0)
                {
                    input = input.Substring(0, lastIndex - retries.ToString().Length - 1);
                }
            }

            return input;
        }
        #endregion

        #region Read from file
        public async Task LoadDataAsync(string csvFilePath)
        {
            if (File.Exists(csvFilePath))
            {
                using (StreamReader reader = new StreamReader(csvFilePath))
                {
                    string line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        string[] pageValues;
                        string[] values = line.Split(',');
                        
                        if (line[0]=='#')
                        {
                            line = line.Replace("#", "");
                            pageValues = line.Split(' ');

                            _pageModel = new PageModel
                            {
                                Site = pageValues[0],
                                Username = pageValues[1],
                                Password = string.Join(" ", pageValues.Skip(2))
                            };

                            _db.OpenConnection();
                            _pageModel.Id = await _db.GetPageIdByAttributes(_pageModel);
                            if(_pageModel.Id == -1)
                            {
                                _pageModel.Id = await _db.AddPageAndReturnId(_pageModel);
                            }
                            _db.CloseConnection();
                        }
                        else if (values.Length >= 1)
                        {
                            string promptTitle = values[0].Trim();
                            // Create a new ArticleModel for each line and set the PromptTitle
                            ArticleModel article = new ArticleModel
                            {
                                PromptTitle = promptTitle,
                                PromptFormat = _basicPrompt,
                                Prompt = _basicPrompt.Replace("{title}", promptTitle),
                                SiteId = _pageModel.Id
                            };
                            _view.Titles.Add(article);
                        }
                    }
                }
            }
            else
            {
                throw new FileNotFoundException("The CSV file does not exist.");
            }
        }
        #endregion
    }
}

