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
        private int _maxRetries;
        private string _defaultPrompt;
        private BindingList<ArticleModel> _titles = new BindingList<ArticleModel>();
        private BindingList<ArticleDatabaseModel> _articleDatabases = new BindingList<ArticleDatabaseModel>();
        private BindingList<PageModel> _pageDatabases = new BindingList<PageModel>();
        Control _uiControl;
        private BindingList<LogsModel> _logs;

        public ArticleView()
        {
            webView2 = new WebView2();
            webView2.Source = new Uri("https://chat.openai.com");

            InitializeComponent();
            generateForSelectedButton.Click += (sender, args) => GenerateArticle?.Invoke(this, EventArgs.Empty);
            generateAllButton.Click += (sender, args) => GenerateForAll?.Invoke(this, EventArgs.Empty);
            //changePromptButton.Click += (sender, args) => ChangeDefaultPrompt?.Invoke(this, EventArgs.Empty);
            importTitlesButton.Click += (sender, args) => ImportTitles?.Invoke(this, EventArgs.Empty);
            addToPageButton.Click += (sender, args) => AddToPageAsync?.Invoke(this, EventArgs.Empty);
            regenerateArticleButton.Click += (sender, args) => RegenarateArticle?.Invoke(this, EventArgs.Empty);
            titlesListBox.SelectedIndexChanged += (sender, args) => SelectedTitleChanged?.Invoke(this, EventArgs.Empty);
            saveSettingsButton.Click += (sender, args) => SaveSettings?.Invoke(this, EventArgs.Empty);
            cancelSettingsButton.Click += (sender, args) => CancelSettings?.Invoke(this, EventArgs.Empty);
            addImagesButton.Click += (sender, args) => AddImages?.Invoke(this, EventArgs.Empty);
            runGenerationButton.Click += (sender, args) => RunGeneration?.Invoke(this, EventArgs.Empty);
            //databaseComboBox.SelectedValueChanged += (sender, args) => DatabaseSelectionChanged?.Invoke(this, EventArgs.Empty);
            //generateFromDatabaseButton.Click += (sender, args) => GenerateFromDatabase?.Invoke(this, EventArgs.Empty);
            browseImagesPathButton.Click += (sender, args) => BrowseImagePath?.Invoke(this, EventArgs.Empty);
            browseExportFilePathButton.Click += (sender, args) => BrowseExportFilePath?.Invoke(this, EventArgs.Empty);
            generateForSelectedButton.Click += (sender, args) => GeneratrForSelected?.Invoke(this, EventArgs.Empty);
            addToPageSelectedButton.Click += (sender, args) => AddToPageSelected?.Invoke(this, EventArgs.Empty);
            importSelectedImagesButton.Click += (sender, args) => ImportSelectedImages?.Invoke(this, EventArgs.Empty);
            createNewFileCheckBox.CheckedChanged += (sender, args) => CreateNewFileChanged?.Invoke(this, EventArgs.Empty);

            promptFormatTextBox.TextChanged += (sender, args) => PromptFormatTextBoxChanged?.Invoke(this, EventArgs.Empty);
            //promptTextBox.TextChanged += (sender, args) => PromptTextBoxChanged?.Invoke(this, EventArgs.Empty);
            titleTextBox.TextChanged += (sender, args) => TitleTextBoxChanged?.Invoke(this, EventArgs.Empty);
            contentTextBox.TextChanged += (sender, args) => ContentTextBoxChanged?.Invoke(this, EventArgs.Empty);
            tagsTextBox.TextChanged += (sender, args) => TagsTextBoxChanged?.Invoke(this, EventArgs.Empty);
            metaTitleTextBox.TextChanged += (sender, args) => MetaTitleTextBoxChanged?.Invoke(this, EventArgs.Empty);
            metaDescriptionTextBox.TextChanged += (sender, args) => MetaDescriptionTextBoxChanged?.Invoke(this, EventArgs.Empty);

            titleNameTextBox.TextChanged += (sender, args) => TitleNameTextBoxChanged?.Invoke(this, EventArgs.Empty);
            contentNameTextBox.TextChanged += (sender, args) => ContentNameTextBoxChanged?.Invoke(this, EventArgs.Empty);
            metaTagsNameTextBox.TextChanged += (sender, args) => TagsNameTextBoxChanged?.Invoke(this, EventArgs.Empty);
            metaTitleNameTextBox.TextChanged += (sender, args) => MetaTitleNameTextBoxChanged?.Invoke(this, EventArgs.Empty);
            metaDescriptionNameTextBox.TextChanged += (sender, args) => MetaDescriptionNameTextBoxChanged?.Invoke(this, EventArgs.Empty);

            saveConfigurationButton.Click += (sender, args) => SaveConfigurationClick?.Invoke(this, EventArgs.Empty);
            cancelConfigurationButton.Click += (sender, args) => CancelConfigurationClick?.Invoke(this, EventArgs.Empty);

            /*
            titleConfigurationTextBox.TextChanged += (sender, args) => TitleConfigurationTextBoxChanged?.Invoke(this, EventArgs.Empty);
            contentConfigurationTextBox.TextChanged += (sender, args) => ContentConfigurationTextBoxChanged?.Invoke(this, EventArgs.Empty);
            tagsConfigurationTextBox.TextChanged += (sender, args) => TagsConfigurationTextBoxChanged?.Invoke(this, EventArgs.Empty);
            metaTitleConfigurationTextBox.TextChanged += (sender, args) => MetaTitleConfigurationTextBoxChanged?.Invoke(this, EventArgs.Empty);
            metaDescriptionConfigurationTextBox.TextChanged += (sender, args) => MetaDescriptionConfigurationTextBoxChanged?.Invoke(this, EventArgs.Empty);
            */

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
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
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

        public int MaxRetries
        {
            get => (int)maxRetriesNumeric.Value;
            set => maxRetriesNumeric.Value = value;
        }

        public string DefaultPrompt
        {
            get => defaultPromptTextBox.Text;
            set => defaultPromptTextBox.Text = value;
        }

        public BindingList<ArticleDatabaseModel> ArticleDatabases
        {
            get => _articleDatabases;
            set
            {
                _articleDatabases = value;
                if (databaseGridView.DataSource != _articleDatabases)
                    databaseGridView.DataSource = _articleDatabases;
            }
        }

        public BindingList<PageModel> PageDatabases
        {
            get => _pageDatabases;
            set
            {
                _pageDatabases = value;
                if (databaseGridView.DataSource != _pageDatabases)
                    databaseGridView.DataSource = _pageDatabases;
            }
        }
        public string ImagesFilePath
        {
            get => imagesPathTextBox.Text;
            set => imagesPathTextBox.Text = value;
        }

        public string ExportFilePath
        {
            get => exportFilePathTextBox.Text;
            set => exportFilePathTextBox.Text = value;
        }
        public string ExportFileName
        {
            get => exportFileNameTextBox.Text;
            set => exportFileNameTextBox.Text = value;
        }
        public bool CreateNewFile
        {
            get => createNewFileCheckBox.Checked;
            set => createNewFileCheckBox.Checked = value;
        }
        public string TitleName
        {
            get => titleNameTextBox.Text;
            set => titleNameTextBox.Text = value;
        }
        public string ContentName
        {
            get => contentNameTextBox.Text;
            set => contentNameTextBox.Text = value;
        }
        public string TagsName
        {
            get => metaTagsNameTextBox.Text;
            set => metaTagsNameTextBox.Text = value;
        }
        public string MetaTitleName
        {
            get => metaTitleNameTextBox.Text;
            set => metaTitleNameTextBox.Text = value;
        }
        public string MetaDescriptionName
        {
            get => metaDescriptionNameTextBox.Text;
            set => metaDescriptionNameTextBox.Text = value;
        }
        public string TitleConfiguration
        {
            get => titleConfigurationTextBox.Text;
            set => titleConfigurationTextBox.Text = value;
        }
        public string ContentConfiguration
        {
            get => contentConfigurationTextBox.Text;
            set => contentConfigurationTextBox.Text = value;
        }
        public string TagsConfiguration
        {
            get => tagsConfigurationTextBox.Text;
            set => tagsConfigurationTextBox.Text = value;
        }
        public string MetaTitleConfiguration
        {
            get => metaTitleConfigurationTextBox.Text;
            set => metaTitleConfigurationTextBox.Text = value;
        }
        public string MetaDescriptionConfiguration
        {
            get => metaDescriptionConfigurationTextBox.Text;
            set => metaDescriptionConfigurationTextBox.Text = value;
        }
        public string MetaTitle
        {
            get => metaTitleTextBox.Text;
            set => metaTitleTextBox.Text = value;
        }
        public string MetaDescription
        {
            get => metaDescriptionTextBox.Text;
            set => metaDescriptionTextBox.Text = value;
        }
        public string ImagePath
        {
            get => imagePictureBox.ImageLocation;
            set => imagePictureBox.ImageLocation = value;
        }
        public BindingList<LogsModel> Logs
        {
            get => _logs;
            set
            {
                _logs = value;
                if (databaseGridView.DataSource != _logs)
                    databaseGridView.DataSource = _logs;
            }
        }

        public bool AddTime
        {
            get => addTimeCheckBox.Checked;
            set => addTimeCheckBox.Checked = value;
        }

        public bool AddTimeEnable
        {
            get => addTimeCheckBox.Enabled;
            set => addTimeCheckBox.Enabled = value;
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
        public event EventHandler TitleTextBoxChanged;
        public event EventHandler ContentTextBoxChanged;
        public event EventHandler TagsTextBoxChanged;
        public event EventHandler SaveSettings;
        public event EventHandler CancelSettings;
        public event EventHandler AddImages;
        public event EventHandler RunGeneration;
        public event EventHandler DatabaseSelectionChanged;
        public event EventHandler GenerateFromDatabase;
        public event EventHandler BrowseImagePath;
        public event EventHandler BrowseExportFilePath;
        public event EventHandler GeneratrForSelected;
        public event EventHandler TitleNameTextBoxChanged;
        public event EventHandler ContentNameTextBoxChanged;
        public event EventHandler TagsNameTextBoxChanged;
        public event EventHandler MetaTitleNameTextBoxChanged;
        public event EventHandler MetaDescriptionNameTextBoxChanged;
        public event EventHandler SaveConfigurationClick;
        public event EventHandler CancelConfigurationClick;
        public event EventHandler MetaTitleTextBoxChanged;
        public event EventHandler MetaDescriptionTextBoxChanged;
        public event EventHandler AddToPageSelected;
        public event EventHandler ImportSelectedImages;
        public event EventHandler CreateNewFileChanged;

        /*
public event EventHandler TitleConfigurationTextBoxChanged;
public event EventHandler ContentConfigurationTextBoxChanged;
public event EventHandler TagsConfigurationTextBoxChanged;
public event EventHandler MetaTitleConfigurationTextBoxChanged;
public event EventHandler MetaDescriptionConfigurationTextBoxChanged;
*/

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
