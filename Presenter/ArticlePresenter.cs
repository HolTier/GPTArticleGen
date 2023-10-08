using GPTArticleGen.Model;
using GPTArticleGen.View;
using Microsoft.Web.WebView2.WinForms;
using System;
using System.Collections;
using System.Collections.Generic;
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

        public ArticlePresenter(IArticleView view, ArticleModel model)
        {
            _view = view;
            _model = model;

            _view.GenerateArticle += GenerateArticle;
            _view.GenerateForAll += GenerateForAll;
            _view.ChangeDefaultPrompt += ChangeDefaultPrompt;
            _view.ImportTitles += ImportTitles;
            _view.AddToPage += AddToPage;
        }

        public void Initialize()
        {
            _view.Title = _model.Title;
            _view.Content = _model.Content;
            _view.Tags = _model.Tags;
            _view.Prompt = _model.Prompt;
            _view.WebView2.Source = new Uri("https://chat.openai.com");

        }

        private void AddToPage(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
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
                    _view.WebView2.Reload();
                }, null);
            });
        }

        static async Task<string> ExecuteJavaScriptAndWaitAsync(WebView2 webView2, string jsCode, string targetAttribute)
        {
            if (webView2 != null)
            {
                try
                {
                    // Execute the JavaScript code
                    await webView2.ExecuteScriptAsync(jsCode);
                    await Task.Delay(20000);
                    string highestNValue = await FindHighestNAsync(webView2);

                    // Wait for the element with the specified attribute
                    string result = await WaitForElementWithAttributeAsync(webView2, targetAttribute, highestNValue);
                    

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
                document.execCommand('insertText', false, 'Wygeneruj testowy artykuł');
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
    }
}

