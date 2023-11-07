using GPTArticleGen.Model;
using GPTArticleGen.View;
using GPTArticleGen.Presenter;
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
using System.Text.RegularExpressions;
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
        //private string _basicPrompt;
        private WordpressRepository _wordpressRepository;
        private PageModel _pageModel;
        private SQLiteDB _db;
        private TaskCompletionSource<bool> _taskCompletionSource;
        private ProgressDialogPresenter progressDialog;
        private List<string> _endMarkersList;

        #region Initialize
        public ArticlePresenter(IArticleView view, ArticleModel model)
        {
            _view = view;
            _model = model;
            _wordpressRepository = new WordpressRepository();   
            _taskCompletionSource = new TaskCompletionSource<bool>();

            //_view.GenerateArticle += GenerateArticle;
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
            _view.AddImages += ImportAllImages;
            _view.RunGeneration += RunGeneration;
            _view.DatabaseSelectionChanged += DatabaseSelectionChanged;
            _view.GenerateFromDatabase += GenerateFromDatabase;
            _view.BrowseImagePath += BrowseImagePath;
            _view.BrowseExportFilePath += BrowseExportFilePath;
            _view.GeneratrForSelected += GenerateForSelected;
            _view.TitleNameTextBoxChanged += TitleNameTextBoxChanged;
            _view.ContentNameTextBoxChanged += ContentNameTextBoxChanged;
            _view.TagsNameTextBoxChanged += TagsNameTextBoxChanged;
            _view.MetaTitleNameTextBoxChanged += MetaTitleNameTextBoxChanged;
            _view.MetaDescriptionNameTextBoxChanged += MetaDescriptionNameTextBoxChanged;
            _view.SaveConfigurationClick += SaveConfigurationClick;
            _view.CancelConfigurationClick += CancelConfigurationClick;
            _view.MetaTitleTextBoxChanged += MetaTitleTextBoxChanged;
            _view.MetaDescriptionTextBoxChanged += MetaDescriptionTextBoxChanged;
            _view.AddToPageSelected += AddToPageSelected;
            _view.ImportSelectedImages += ImportSelectedImages;
            _view.CreateNewFileChanged += CreateNewFileChanged;
            /*
            _view.TitleConfigurationTextBoxChanged += TitleConfigurationTextBoxChanged;
            _view.ContentConfigurationTextBoxChanged += ContentConfigurationTextBoxChanged;
            _view.TagsConfigurationTextBoxChanged += TagsConfigurationTextBoxChanged;
            _view.MetaTitleConfigurationTextBoxChanged += MetaTitleConfigurationTextBoxChanged;
            _view.MetaDescriptionConfigurationTextBoxChanged += MetaDescriptionConfigurationTextBoxChanged;
            */
        }

        public async void Initialize()
        {
            // Initialize your view with the data from the model
            _view.Title = _model.Title;
            _view.Content = _model.Content;
            //_view.Tags = _model.Tags;
            //_view.Prompt = _model.Prompt;
            // _view.Description = _model.Description;
            _view.WebView2.Source = new Uri("https://chat.openai.com");
            //_view.Tags = new ObservableCollection<string>();

            // Initialize SQLiteDB
            _db = new SQLiteDB();
            _view.Logs = await _db.GetAllLogs();

            //_basicPrompt = Properties.Settings.Default.BasicPrompt;
            //Debug.WriteLine("Basic prompt: " + _basicPrompt);
            //Debug.WriteLine("Propertise basic prompt:" + Properties.Settings.Default.BasicPrompt);
            //_view.DefaultPrompt = _basicPrompt;
            _view.MaxRetries = Properties.Settings.Default.MaxRetries;
            _view.ImagesFilePath = Properties.Settings.Default.ImagesPath;
            _view.ExportFilePath = Properties.Settings.Default.ExportFilePath;
            _view.ExportFileName = Properties.Settings.Default.ExportFileName;
            _view.CreateNewFile = Properties.Settings.Default.CreateNewFile;
            _view.AddTime = Properties.Settings.Default.AddTime;

            // Configuration default
            _view.DefaultPrompt = Properties.Settings.Default.DefaultPrompt;
            _view.TitleConfiguration = Properties.Settings.Default.TitleName;
            _view.ContentConfiguration = Properties.Settings.Default.ContentName;
            _view.TagsConfiguration = Properties.Settings.Default.MetaTagsName;
            _view.MetaTitleConfiguration = Properties.Settings.Default.MetaTitleName;
            _view.MetaDescriptionConfiguration = Properties.Settings.Default.MetaDescriptionName;

        }
        #endregion

        #region Event Handlers
        private async void AddToPageAsync(object? sender, EventArgs e)
        {
            List<ArticleModel> articles = new List<ArticleModel>(_view.Titles);

            if(articles.Count > 0)
                await AddToPageFunctionAsync(articles);
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

        private async void GenerateForAll(object? sender, EventArgs e)
        {
            try
            {
                await Task.Run(async () =>
                {
                    // Run your time-consuming tasks here
                    List<ArticleModel> articles = new List<ArticleModel>(_view.Titles);
                    _taskCompletionSource = new TaskCompletionSource<bool>();
                    await GenerateForAllFunctionAsync(articles);
                    _taskCompletionSource.Task.Wait();  // Wait for task to complete
                });
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Debug.WriteLine(ex);
            }
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

                    selected.Title = await ExtractValueBetweenAsync(selected.RawData, "Meta title:", _endMarkersList);
                    selected.Content = await ExtractValueBetweenAsync(selected.RawData, "Meta content:", _endMarkersList);
                    selected.Tags = await ExtractTagsAsync(selected.RawData, _view.TagsName, _endMarkersList);

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
                _view.MetaTitle = selectedArticle.MetaTitle;
                _view.MetaDescription = selectedArticle.MetaDescription;
                _view.PromptFormat = selectedArticle.PromptFormat;
                _view.TitleName = selectedArticle.TitleName;
                _view.ContentName = selectedArticle.ContentName;
                _view.TagsName = selectedArticle.TagsName;
                _view.MetaTitleName = selectedArticle.MetaTitleName;
                _view.MetaDescriptionName = selectedArticle.MetaDescriptionName;
                _view.ImagePath = selectedArticle.ImagePath;
                //_view.Prompt = selectedArticle.Prompt;
               
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
            _view.ImagesFilePath = Properties.Settings.Default.ImagesPath;
            _view.ExportFilePath = Properties.Settings.Default.ExportFilePath;
            _view.ExportFileName = Properties.Settings.Default.ExportFileName;
            _view.CreateNewFile = Properties.Settings.Default.CreateNewFile;
            _view.AddTime = Properties.Settings.Default.AddTime;
        }

        private void SaveSettings(object? sender, EventArgs e)
        {
            // Assign your new values
            Properties.Settings.Default.BasicPrompt = _view.DefaultPrompt;
            Properties.Settings.Default.MaxRetries = _view.MaxRetries;
            Properties.Settings.Default.ImagesPath = _view.ImagesFilePath;
            Properties.Settings.Default.ExportFilePath = _view.ExportFilePath;
            Properties.Settings.Default.ExportFileName = _view.ExportFileName;
            Properties.Settings.Default.CreateNewFile = _view.CreateNewFile;
            Properties.Settings.Default.AddTime = _view.AddTime;

            // Save the changes
            Properties.Settings.Default.Save();
        }

        private async void ImportAllImages(object? sender, EventArgs e)
        {
            List<ArticleModel> articles = new List<ArticleModel>(_view.Titles);

            await AddImagesFunctionAsync(articles);

            SelectedTitleChanged(this, EventArgs.Empty);
        }

        private async void RunGeneration(object? sender, EventArgs e)
        {
            try
            {
                ProgressDialogView progressDialogView = new ProgressDialogView();
                progressDialogView.Show();  // Show the dialog non-modally

                progressDialog = new ProgressDialogPresenter(progressDialogView, _view.Titles.Count, _view.Titles.Count, _view.Titles.Count);
                progressDialog.Initialize();

                await Task.Run(() =>
                {
                    // Run your time-consuming tasks here
                    ImportAllImages(sender, e);

                    _taskCompletionSource = new TaskCompletionSource<bool>();
                    GenerateForAll(sender, e);
                    _taskCompletionSource.Task.Wait();  // Wait for task to complete

                    AddToPageAsync(sender, e);
                });
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Debug.WriteLine(ex);
            }
        }

        private async void DatabaseSelectionChanged(object? sender, EventArgs e)
        {
            
        }

        private async void GenerateFromDatabase(object? sender, EventArgs e)
        {
            
        }

        private void BrowseExportFilePath(object? sender, EventArgs e)
        {
            // Create and configure the FolderBrowserDialog
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog
            {
                Description = "Select a folder to export the data to",
                UseDescriptionForTitle = true,
                InitialDirectory = String.IsNullOrEmpty(Properties.Settings.Default.ExportFilePath) ? 
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop) : Properties.Settings.Default.ExportFilePath,
                ShowNewFolderButton = true
            };

            // Show the dialog and get the selected folder path
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFolderPath = folderBrowserDialog.SelectedPath;
                   
            } 
        }

        private void BrowseImagePath(object? sender, EventArgs e)
        {
            // Create and configure the FolderBrowserDialog
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog
            {
                Description = "Select a folder to import the images from",
                UseDescriptionForTitle = true,
                InitialDirectory = String.IsNullOrEmpty(Properties.Settings.Default.ImagesPath) ?
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop) : Properties.Settings.Default.ImagesPath,
                ShowNewFolderButton = true
            };

            // Show the dialog and get the selected folder path
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFolderPath = folderBrowserDialog.SelectedPath;
                _view.ImagesFilePath = selectedFolderPath;
            }
        }

        private async void GenerateForSelected(object? sender, EventArgs e)
        {
            try
            {
                ArticleModel selected = _view.Titles.FirstOrDefault(item => item == _view.SelectedTitle);
                List<ArticleModel> articles = new List<ArticleModel>() { selected};
                await Task.Run(async () =>
                {
                    // Run your time-consuming tasks here
                    
                    _taskCompletionSource = new TaskCompletionSource<bool>();
                    await GenerateForAllFunctionAsync(articles);
                    _taskCompletionSource.Task.Wait();  // Wait for task to complete
                });
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Debug.WriteLine(ex);
            }
        }

        private void MetaDescriptionNameTextBoxChanged(object? sender, EventArgs e)
        {
            ArticleModel selectedArticle = _view.Titles.FirstOrDefault(item => item == _view.SelectedTitle);

            if(selectedArticle != null)
            {
                selectedArticle.MetaDescriptionName = _view.MetaDescriptionName;
            }
        }

        private void MetaTitleNameTextBoxChanged(object? sender, EventArgs e)
        {
            ArticleModel selectedArticle = _view.Titles.FirstOrDefault(item => item == _view.SelectedTitle);

            if(selectedArticle != null)
            {
                selectedArticle.MetaTitleName = _view.MetaTitleName;
            }
        }

        private void TagsNameTextBoxChanged(object? sender, EventArgs e)
        {
            ArticleModel selectedArticle = _view.Titles.FirstOrDefault(item => item == _view.SelectedTitle);

            if(selectedArticle != null)
            {
                selectedArticle.TagsName = _view.TagsName;
            }
        }

        private void ContentNameTextBoxChanged(object? sender, EventArgs e)
        {
            ArticleModel selectedArticle = _view.Titles.FirstOrDefault(item => item == _view.SelectedTitle);

            if(selectedArticle != null)
            {
                selectedArticle.ContentName = _view.ContentName;
            }
        }

        private void TitleNameTextBoxChanged(object? sender, EventArgs e)
        {
            ArticleModel selectedArticle = _view.Titles.FirstOrDefault(item => item == _view.SelectedTitle);

            if(selectedArticle != null)
            {
                selectedArticle.TitleName = _view.TitleName;
            }
        }

        private void CancelConfigurationClick(object? sender, EventArgs e)
        {
            _view.DefaultPrompt = Properties.Settings.Default.DefaultPrompt;
            _view.TitleConfiguration = Properties.Settings.Default.TitleName;
            _view.ContentConfiguration = Properties.Settings.Default.ContentName;
            _view.TagsConfiguration = Properties.Settings.Default.MetaTagsName;
            _view.MetaTitleConfiguration = Properties.Settings.Default.MetaTitleName;
            _view.MetaDescriptionConfiguration = Properties.Settings.Default.MetaDescriptionName;
        }

        private void SaveConfigurationClick(object? sender, EventArgs e)
        {
            Properties.Settings.Default.DefaultPrompt = _view.DefaultPrompt;
            Properties.Settings.Default.TitleName = _view.TitleConfiguration;
            Properties.Settings.Default.ContentName = _view.ContentConfiguration;
            Properties.Settings.Default.MetaTagsName = _view.TagsConfiguration;
            Properties.Settings.Default.MetaTitleName = _view.MetaTitleConfiguration;
            Properties.Settings.Default.MetaDescriptionName = _view.MetaDescriptionConfiguration;

            Properties.Settings.Default.Save();

            foreach(ArticleModel article in _view.Titles)
            {
                article.TitleName = Properties.Settings.Default.TitleName;
                article.ContentName = Properties.Settings.Default.ContentName;
                article.TagsName = Properties.Settings.Default.MetaTagsName;
                article.MetaTitleName = Properties.Settings.Default.MetaTitleName;
                article.MetaDescriptionName = Properties.Settings.Default.MetaDescriptionName;
            }

            SelectedTitleChanged(this, EventArgs.Empty);
        }

        private void MetaDescriptionTextBoxChanged(object? sender, EventArgs e)
        {
            ArticleModel article = _view.Titles.FirstOrDefault(item => item == _view.SelectedTitle);

            if(article != null)
            {
                article.MetaDescription = _view.MetaDescription;
            }
        }

        private void MetaTitleTextBoxChanged(object? sender, EventArgs e)
        {
            ArticleModel article = _view.Titles.FirstOrDefault(item => item == _view.SelectedTitle);

            if(article != null)
            {
                article.MetaTitle = _view.MetaTitle;
            }
        }

        private async void AddToPageSelected(object? sender, EventArgs e)
        {
            if(_view.SelectedTitle != null)
                await AddToPageFunctionAsync(new List<ArticleModel>() { _view.SelectedTitle });
        }

        private async void ImportSelectedImages(object? sender, EventArgs e)
        {
            ArticleModel article = _view.Titles.FirstOrDefault(item => item == _view.SelectedTitle);

            if(article != null)
                await AddImagesFunctionAsync(new List<ArticleModel>() { article });

            SelectedTitleChanged(this, EventArgs.Empty);
        }

        private void CreateNewFileChanged(object? sender, EventArgs e)
        {
            _view.AddTimeEnable = _view.CreateNewFile;
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
        static async Task<string> ExtractValueBetweenAsync(string text, string startMarker, List<string> endMarkers)
        {
            int startIndex = text.IndexOf(startMarker, StringComparison.CurrentCultureIgnoreCase); // Case-insensitive search for startMarker
            if (startIndex >= 0)
            {
                string originalStartMarker = text.Substring(startIndex, startMarker.Length); // Get the original case startMarker

                startIndex += originalStartMarker.Length; // Move past the original case startMarker
                int minEndIndex = -1; // Initialize the minimum end index to a value that indicates none were found

                foreach (string endMarker in endMarkers)
                {
                    if (string.Equals(endMarker, originalStartMarker, StringComparison.CurrentCultureIgnoreCase))
                    {
                        continue; // Skip the startMarker
                    }

                    int endIndex = text.IndexOf(endMarker, startIndex, StringComparison.CurrentCultureIgnoreCase); // Case-insensitive search for endMarker
                    if (endIndex >= 0)
                    {
                        if (minEndIndex == -1 || endIndex < minEndIndex)
                        {
                            minEndIndex = endIndex;
                        }
                    }
                }

                if (minEndIndex >= 0)
                {
                    Debug.WriteLine("Start index: " + (minEndIndex - startIndex));
                    Debug.WriteLine("Text: " + text.Substring(startIndex, minEndIndex - startIndex).Trim());
                    return text.Substring(startIndex, minEndIndex - startIndex).Trim();
                }

                // If no endMarker is found, return from startIndex to the end of the text
                return text.Substring(startIndex).Trim();
            }

            return null; // Handle error if startMarker is not found
        }


        static async Task<string> ExtractTagsAsync(string text, string startMarker, List<string> endMarkers)
        {
            int startIndex = text.IndexOf(startMarker);
            
            if (startIndex >= 0)
            {
                string tagSection;
                string[] tags;
                startIndex += startMarker.Length;
                int minEndIndex = -1; // Initialize the minimum end index to a value that indicates none were found
                foreach (string endMarker in endMarkers)
                {
                    int endIndex = text.IndexOf(endMarker, startIndex);
                    if (endIndex >= 0)
                    {
                        if (minEndIndex == -1 || endIndex < minEndIndex)
                        {
                            minEndIndex = endIndex;
                        }
                    }
                }
                if (minEndIndex >= 0)
                {
                    tagSection = text.Substring(startIndex, minEndIndex - startIndex).Trim();
                    tags = tagSection.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < tags.Length; i++)
                    {
                        tags[i] = tags[i].Trim();
                    }
                    return string.Join(", ", tags);
                }
                // If no end marker is found, return from startIndex to the end of the text
                tagSection = text.Substring(startIndex).Trim();
                tags = tagSection.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < tags.Length; i++)
                {
                    tags[i] = tags[i].Trim();
                }
                return string.Join(", ", tags);
            }
            return null; // Handle error if start marker is not found
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
                    //input = input.Substring(0, lastIndex - retries.ToString().Length - 1);
                }
            }

            return input;
        }
        #endregion

        #region File Operations Methods
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
                            pageValues = line.Split('|');

                            _pageModel = new PageModel
                            {
                                Site = pageValues[0],
                                Username = pageValues[1],
                                Password = pageValues[2]
                            };
                        }
                        else if (values.Length >= 1)
                        {
                            //_db.OpenConnection();
                            string promptTitle = values[0].Trim();
                            // Create a new ArticleModel for each line and set the PromptTitle
                            ArticleModel article = new ArticleModel
                            {
                                PromptTitle = promptTitle,
                                PromptFormat = Properties.Settings.Default.DefaultPrompt,
                                Prompt = Properties.Settings.Default.DefaultPrompt,
                                SiteId = _pageModel.Id,
                                IsPublished = false,
                                TitleName = Properties.Settings.Default.TitleName,
                                ContentName = Properties.Settings.Default.ContentName,
                                TagsName = Properties.Settings.Default.MetaTagsName,
                                MetaTitleName = Properties.Settings.Default.MetaTitleName,
                                MetaDescriptionName = Properties.Settings.Default.MetaDescriptionName,
                                Site = _pageModel.Site,
                                Username = _pageModel.Username,
                                Password = _pageModel.Password
                            };
                            
                            _view.Titles.Add(article);
                            
                            //_db.CloseConnection();
                        }
                    }
                }
            }
            else
            {
                throw new FileNotFoundException("The CSV file does not exist.");
            }
        }

        public async Task SaveDataAsync()
        {
            if (!string.IsNullOrEmpty(_view.ExportFilePath) && !string.IsNullOrEmpty(_view.ExportFileName))
            {
                string fullFilePath = Path.Combine(_view.ExportFilePath, _view.ExportFileName + ".csv");

                if (File.Exists(fullFilePath) && !Properties.Settings.Default.CreateNewFile)
                {
                    using (StreamWriter writer = File.AppendText(fullFilePath))
                    {
                        foreach (ArticleModel article in _view.Titles)
                        {
                            string line = string.Join(",",new string[] { article.PromptTitle, article.Site, article.Username, article.PostUrl });
                            if (article.IsPublished)
                                await writer.WriteLineAsync(line);
                        }
                    }
                }
                else
                {
                    if (!Properties.Settings.Default.AddTime)
                    {
                        int fileNumber = 0;
                        string exportName = _view.ExportFileName;
                        while (File.Exists(fullFilePath))
                        {
                            exportName = $"{_view.ExportFileName}({fileNumber}).csv";
                            fullFilePath = Path.Combine(_view.ExportFilePath, exportName);
                            fileNumber++;
                        }
                    }
                    else
                    {
                        fullFilePath = Path.Combine(_view.ExportFilePath, 
                            $"{_view.ExportFileName}_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}.csv");
                    }
                    using (StreamWriter writer = new StreamWriter(fullFilePath))
                    {
                        string header = string.Join(",", new string[] { "Title", "Site", "Username", "PostUrl" });
                        await writer.WriteLineAsync(header);
                        foreach (ArticleModel article in _view.Titles)
                        {
                            string line = string.Join(",", new string[] { article.PromptTitle, article.Site, article.Username, article.PostUrl });
                            if (article.IsPublished)
                                await writer.WriteLineAsync(line);
                        }
                    }
                }
            }
            else
            {
                throw new FileNotFoundException("The ExportFilePath or ExportFileName is not set.");
            }
        }
        #endregion

        #region Adding

        public async Task AddToPageFunctionAsync(List<ArticleModel> articles)
        {
            // Initialize SQLiteDB
            //SQLiteDB db = new SQLiteDB();

            int i = 0;
            if (progressDialog != null)
                progressDialog.UpdateAddToPageProgress(i);
            foreach (ArticleModel article in articles)
            {
                // Create an object to hold your article data
                var articleData = new
                {
                    title = article.Title,
                    content = article.Content,
                    status = "publish",
                    tags = new[] { "[tag]" },
                    featured_media = !string.IsNullOrEmpty(article.ImagePath) ? "[featured_image]" : null,
                    yoast_meta = new
                    {
                        yoast_wpseo_title = article.MetaTitle,
                        yoast_wpseo_metadesc = article.MetaDescription
                    }
                };

                // Serialize the object to JSON
                article.PostData = JsonConvert.SerializeObject(articleData);

                article.PostData = article.PostData.Replace("\"[tag]\"", "[tag]");

                List<string> tags = new List<string>();

                // Get tags
                if (!String.IsNullOrEmpty(article.Tags))
                    tags = article.Tags.Split(", ").ToList();

                //Add to wordpress
                if (await _wordpressRepository.AddPostAsync(tags, article))
                {
                    //Add to database
                    article.IsPublished = true;
                    LogsModel logs = new LogsModel()
                    {
                        PromptTitle = article.PromptTitle,
                        Site = article.Site,
                        Username = article.Username,
                        PostUrl = article.PostUrl,
                        PostId = article.Id,
                        Date = article.Date
                    };
                    await _db.InsertLogAsync(logs);
                    //await _db.UpdateArticleWithoutImageIdAsync(article);
                    _view.Logs = await _db.GetAllLogs();

                    // Save to file
                    await SaveDataAsync();
                }
                else
                {
                    Debug.WriteLine("Failed to add post to wordpress. Post title: " + article.Title);
                }

                if (progressDialog != null)
                    progressDialog.UpdateAddToPageProgress(++i);

                //await db.InsertArticleAsync(article);
            }
        }

        public async Task GenerateForAllFunctionAsync(List<ArticleModel> articles)
        {
            bool isFirst = true;

            var taskCompletionSource = new TaskCompletionSource<bool>();

            await Task.Run(async () =>
            {
                // Update the UI with the result on the UI thread
                Program.SyncContext.Post(async _ =>
                {
                    //_view.DisableUI();

                    int i = 0;
                    if (progressDialog != null)
                        progressDialog.UpdateGenerateArticleProgress(i);
                    foreach (ArticleModel selected in articles)
                    {
                        List<string> endMarkers = new List<string>() 
                            { selected.TitleName, selected.ContentName, selected.TagsName, selected.MetaTitleName, selected.MetaDescriptionName };

                        while (selected.Retries <= _view.MaxRetries)
                        {
                            if (isFirst)
                            {
                                await GenerateByGPTAsync(_view.WebView2, _view, selected.PromptFormat.Replace("{title}", selected.PromptTitle), selected);
                                isFirst = false;
                            }
                            else
                                await EditRegenerateByGPTAsync(_view.WebView2, _view, selected.PromptFormat.Replace("{title}", selected.PromptTitle), selected);

                            selected.Title = await ExtractValueBetweenAsync(selected.RawData, selected.TitleName, endMarkers);
                            selected.Content = await ExtractValueBetweenAsync(selected.RawData, selected.ContentName, endMarkers);
                            selected.Tags = await ExtractValueBetweenAsync(selected.RawData, selected.TagsName, endMarkers);
                            selected.MetaTitle = await ExtractValueBetweenAsync(selected.RawData, selected.MetaTitleName, endMarkers);
                            selected.MetaDescription = await ExtractValueBetweenAsync(selected.RawData, selected.MetaDescriptionName, endMarkers);

                            selected.Tags = SubstreingFromString(selected.Tags, selected.Retries);

                            if (selected.Retries == 1 && progressDialog != null)
                                progressDialog.UpdateGenerateArticleProgress(++i);

                            selected.Retries++;

                            SelectedTitleChanged(this, EventArgs.Empty);

                            await Task.Delay(TimeSpan.FromSeconds(2));

                            if (!String.IsNullOrEmpty(selected.Title) && !String.IsNullOrEmpty(selected.Content) && !String.IsNullOrEmpty(selected.Tags))
                            {
                                break;
                            }
                        }
                    }

                    // Signal that the work is done
                    taskCompletionSource.SetResult(true);

                    //_view.EnableUI();
                }, null);
            });

            // Wait for the Task.Run to complete
            await taskCompletionSource.Task;
            _taskCompletionSource.SetResult(true);
            // Wait for all tasks to complete
            Debug.WriteLine("Generation for all finished");
        }

        public async Task AddImagesFunctionAsync(List<ArticleModel> articles)
        {
            int i = 0;
            if (progressDialog != null)
                progressDialog.UpdateAddImagesProgress(i);
            foreach (ArticleModel article in articles)
            {
                // Modify the article title to replace spaces with hyphens and make it lowercase.
                string modifiedTitle = article.PromptTitle.Replace(" ", "-").ToLower();

                // Comment if you want use this symbols as file name
                modifiedTitle = modifiedTitle.Replace("?", "");
                modifiedTitle = modifiedTitle.Replace("!", "");
                modifiedTitle = modifiedTitle.Replace(".", "");
                modifiedTitle = modifiedTitle.Replace(",", "");
                modifiedTitle = modifiedTitle.Replace(":", "");

                // Directory path where your images are stored.
                string imageDirectory = @"C:\Users\holcm\Desktop\images";

                // Get a list of all files in the directory.
                string[] files = Directory.GetFiles(imageDirectory);

                // Define a regular expression to match image files with any extension.
                Regex imageFileRegex = new Regex($"{modifiedTitle}\\.(\\w+)", RegexOptions.IgnoreCase);

                // Iterate through files.
                foreach (string file in files)
                {
                    string fileName = Path.GetFileName(file);
                    // Check if the file name matches the modified article title and is an image file.
                    Match match = imageFileRegex.Match(Path.GetFileName(fileName));
                    if (match.Success)
                    {
                        // You found an image file. You can use it here.
                        string fileExtension = match.Groups[1].Value;
                        Debug.WriteLine($"Found image for article: {article.PromptTitle}. File: {file}. Extension: {fileExtension}");
                        article.ImagePath = file;
                    }
                }
                if (progressDialog != null)
                    progressDialog.UpdateAddImagesProgress(++i);
            }
        }

        #endregion
    }
}

