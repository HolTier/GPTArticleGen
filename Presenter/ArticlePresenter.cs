using GPTArticleGen.Model;
using GPTArticleGen.View;
using Microsoft.Web.WebView2.WinForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPTArticleGen.Presenter
{
    internal class ArticlePresenter
    {
        private readonly IArticleView _view;
        private readonly ArticleModel _model;

        #region Initialize
        public ArticlePresenter(IArticleView view, ArticleModel model)
        {
            _view = view;
            _model = model;

            _view.GenerateArticle += GenerateArticle;
            _view.GenerateForAll += GenerateForAll;
            _view.ChangeDefaultPrompt += ChangeDefaultPrompt;
            _view.ImportTitles += ImportTitles;
            _view.AddToPage += AddToPage;
            _view.RegenarateArticle += RegenarateArticle;
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
            string connectionString = "Data Source=ArticleDatabase.db;Version=3;";
            SQLiteDB db = new SQLiteDB(connectionString);
            db.OpenConnection();
            
            // Create table if not exists
            string createTableQuery = @"CREATE TABLE IF NOT EXISTS Articles (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                title TEXT NOT NULL,
                content TEXT NOT NULL,
                tags TEXT NOT NULL,
                prompt TEXT NOT NULL,
                description TEXT NOT NULL
            )";
            SQLiteCommand createTableCommand = db.CreateCommand();
            createTableCommand.CommandText = createTableQuery;
            createTableCommand.ExecuteNonQuery();

            // Insert data
            string insertDataQuery = @"INSERT INTO Articles (title, content, tags, prompt, description) VALUES (@title, @content, @tags, @prompt, @description)";
            SQLiteCommand insertDataCommand = db.CreateCommand();
            insertDataCommand.CommandText = insertDataQuery;
            insertDataCommand.Parameters.AddWithValue("@title", "test");
            insertDataCommand.Parameters.AddWithValue("@content", "test");
            insertDataCommand.Parameters.AddWithValue("@tags", "test");
            insertDataCommand.Parameters.AddWithValue("@prompt", "test");
            insertDataCommand.Parameters.AddWithValue("@description", "test");
            insertDataCommand.ExecuteNonQuery();

            // Read data
            string readDataQuery = @"SELECT * FROM Articles";
            SQLiteCommand readDataCommand = db.CreateCommand();
            readDataCommand.CommandText = readDataQuery;
            SQLiteDataReader reader = readDataCommand.ExecuteReader();
            while (reader.Read())
            {
                Debug.WriteLine(reader["title"]);
                Debug.WriteLine(reader["content"]);
                Debug.WriteLine(reader["tags"]);
                Debug.WriteLine(reader["prompt"]);
                Debug.WriteLine(reader["description"]);
            }


            db.CloseConnection();

        }
        #endregion

        #region Event Handlers
        private void AddToPage(object? sender, EventArgs e)
        {
            Debug.WriteLine(_view.Tags.Count);
            _view.Tags.Add("test3");
        }

        private void ImportTitles(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ChangeDefaultPrompt(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void GenerateForAll(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RegenarateArticle(object? sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                // Update the UI with the result on the UI thread
                Program.SyncContext.Post(async _ =>
                {
                    await RegenarateByGPTAsync(_view.WebView2, _view);
                    //_view.WebView2.Reload();
                }, null);
            });
        }

        private void GenerateArticle(object? sender, EventArgs e)
        {
            //_view.Content = _view.Prompt;
            //_model.Content = _view.Content;
            //_model.Prompt = _view.Prompt;
            Task.Run(async () =>
            {
                // Update the UI with the result on the UI thread
                Program.SyncContext.Post(async _ =>
                {
                    await GenerateByGPTAsync(_view.WebView2, _view);
                    //_view.WebView2.Reload();
                    _view.Prompt = await ExtractValueBetweenAsync(_view.Content, "Meta title:", "Meta content:");
                    _view.Description = await ExtractValueBetweenAsync(_view.Content, "Meta content:", "Meta tags:");
                    _view.Tags.Add(await ExtractTagsAsync(_view.Content, "Meta tags:"));
                }, null);
            });
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
                    await Task.Delay(TimeSpan.FromSeconds(30));
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

            await Task.Delay(000);
            while (currentRetry < maxRetries)
            {
                result = await webView2.ExecuteScriptAsync(
                    $@"document.querySelector('[data-testid^=""{highestNValue}""]')?.textContent"
                );

                if (!string.IsNullOrEmpty(result))
                {
                    return result;
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

        static async Task GenerateByGPTAsync(WebView2 webView2, IArticleView view)
        {
            string jsCode = @"
                let inputElement = document.getElementById('prompt-textarea');
                inputElement.focus();
                document.execCommand('insertText', false, 'Napisz artykuł na temat ""Chomiki głębinowe. Czy zagrażają ludzią."" na 1500 do 2000 znaków, artykuł podziel na trzy części Meta title:, Meta content:, Meta tags:. Gdzie w Meta content znajduje się cała treść.  Nie dodawaj nic poza tym, wszystko musi znajdować się w jednej z tych części. Nie zapomnij o Meta tags na końcu!!!!');
                document.querySelector('[data-testid=""send-button""]').disabled = false;
                document.querySelector('[data-testid=""send-button""]').click();
                document.querySelector('[data-testid=""send-button""]').disabled = false;
            ";

            string targetAttribute = "conversation-turn-"; // You can set the target attribute here

            string result = await ExecuteJavaScriptAndWaitAsync(webView2, jsCode, targetAttribute);

            if (!string.IsNullOrEmpty(result))
            {
                view.Content = result;
            }
            else
            {
                Console.WriteLine("No matching element found.");
            }
        }

        static async Task RegenarateByGPTAsync(WebView2 webView2, IArticleView view)
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
                view.Content = result;
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
        #endregion
    }
}

