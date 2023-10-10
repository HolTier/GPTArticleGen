using GPTArticleGen.Model;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPTArticleGen.View
{
    public partial class ArticleView : Form, IArticleView
    {
        private string _title;
        private string _description;
        private BindingList<ArticleModel> _titles = new BindingList<ArticleModel>();
        Control _uiControl;


        public ArticleView()
        {
            webView2 = new WebView2();
            webView2.Source = new Uri("https://chat.openai.com");

            InitializeComponent();
            generateButton.Click += (sender, args) => GenerateArticle?.Invoke(this, EventArgs.Empty);
            generateAllButton.Click += (sender, args) => GenerateForAll?.Invoke(this, EventArgs.Empty);
            changePromptButton.Click += (sender, args) => ChangeDefaultPrompt?.Invoke(this, EventArgs.Empty);
            importTitlesButton.Click += (sender, args) => ImportTitles?.Invoke(this, EventArgs.Empty);
            addToPageButton.Click += (sender, args) => AddToPageAsync?.Invoke(this, EventArgs.Empty);
            regenerateArticleButton.Click += (sender, args) => RegenarateArticle?.Invoke(this, EventArgs.Empty);
            titlesListBox.SelectedIndexChanged += (sender, args) => SelectedTitleChanged?.Invoke(this, EventArgs.Empty);
            promptFormatTextBox.TextChanged += (sender, args) => PromptFormatTextBoxChanged?.Invoke(this, EventArgs.Empty);
            promptTextBox.TextChanged += (sender, args) => PromptTextBoxChanged?.Invoke(this, EventArgs.Empty);

            titlesListBox.DataSource = _titles;
            titlesListBox.DisplayMember = "PromptTitle";


            InitializeWebView2();
        }

        private async void InitializeWebView2()
        {
            if (webView2 != null)
            {
                // Subscribe to the CoreWebView2InitializationCompleted event
                webView2.CoreWebView2InitializationCompleted += WebView2InitializationCompleted;

                // Ensure the WebView2 control is initialized
                await webView2.EnsureCoreWebView2Async();
            }
        }

        private async void WebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            if (e.IsSuccess)
            {
                // Now, you can access webView2Control.CoreWebView2 without it being null
                // Perform your actions with the initialized CoreWebView2 here
                // await GenerateByGPTAsync(webView2);
            }
            else
            {
                // Handle initialization failure, if needed
            }
        }

        private void ArticleView_Load(object sender, EventArgs e)
        {
            InitializeWebView2();
        }

        public string Title
        {
            get => titleTextBox.Text;
            set => titleTextBox.Text = value;
            
        }
        public string Content
        {
            get => contentTextBox.Text;
            set => contentTextBox.Text = value;
        }
        public string Tags
        {
            get => tagsTextBox.Text;
            set => tagsTextBox.Text = value;

        }
        public string Prompt
        {
            get => promptTextBox.Text;
            set => promptTextBox.Text = value;
        }

        public WebView2 WebView2
        {
            get => webView2;
            set => webView2 = value;
        }

        public Control UiControl => UiControl;

        public string Description
        {
            get => contentTextBox.Text;
            set => contentTextBox.Text = value;
        }
        public BindingList<ArticleModel> Titles
        {
            get => _titles;
            set
            {
                _titles = value;
                titlesListBox.DataSource = _titles;
            }
        }

        public ArticleModel SelectedTitle
        {
            get => titlesListBox.SelectedItem as ArticleModel;
            set
            {
                // Set the selected item based on the provided title
                for (int i = 0; i < _titles.Count; i++)
                {
                    if (_titles[i] == value)
                    {
                        titlesListBox.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        public string PromptFormat 
        { 
            get => promptFormatTextBox.Text; 
            set => promptFormatTextBox.Text = value; 
        }

        public event EventHandler GenerateArticle;
        public event EventHandler GenerateForAll;
        public event EventHandler ChangeDefaultPrompt;
        public event EventHandler ImportTitles;
        public event EventHandler AddToPageAsync;
        public event EventHandler RegenarateArticle;
        public event EventHandler SelectedTitleChanged;
        public event EventHandler PromptFormatTextBoxChanged;
        public event EventHandler PromptTextBoxChanged;

        public void EnableUI()
        {
            // Enable UI elements as needed
            foreach (Control control in this.Controls)
            {
                control.Enabled = true;
            }
        }

        public void DisableUI()
        {
            // Disable UI elements as needed
            foreach (Control control in this.Controls)
            {
                control.Enabled = false;
            }
        }
    }
}
