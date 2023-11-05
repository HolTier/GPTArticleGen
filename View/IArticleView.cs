using GPTArticleGen.Model;
using Microsoft.Web.WebView2.WinForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPTArticleGen.View
{
    public interface IArticleView
    {
        // Properties
        string Title { get; set; }
        string Content { get; set; }
        string Tags { get; set; }
        string Prompt { get; set; }

        string Description { get; set; }
        string PromptFormat { get; set; }
        ArticleModel SelectedTitle { get; set; }
        WebView2 WebView2 { get; set; }
        Control UiControl { get; }
        BindingList<ArticleModel> Titles { get; set; }
        BindingList<ArticleDatabaseModel> ArticleDatabases { get; set; }
        BindingList<PageModel> PageDatabases { get; set; }
        string DatabaseComboBoxSelectedItem { get; set; }

        // Configurations
        string TitleName { get; set; }
        string ContentName { get; set; }
        string TagsName { get; set; }
        string MetaTitleName { get; set; }
        string MetaDescriptionName { get; set; }

        // Settings
        int MaxRetries { get; set; }
        string DefaultPrompt { get; set; }
        string ImagesFilePath { get; set; }
        string ExportFilePath { get; set; }
        string ExportFileName { get; set; }
        bool CreateNewFile { get; set; }

        // Events
        event EventHandler GenerateArticle;
        event EventHandler GenerateForAll;
        event EventHandler ChangeDefaultPrompt;
        event EventHandler ImportTitles;
        event EventHandler AddToPageAsync;
        event EventHandler RegenarateArticle;
        event EventHandler SelectedTitleChanged;
        event EventHandler SaveSettings;
        event EventHandler CancelSettings;
        event EventHandler BrowseImagePath;
        event EventHandler BrowseExportFilePath;
        event EventHandler AddImages;
        event EventHandler RunGeneration;
        event EventHandler DatabaseSelectionChanged;
        event EventHandler GenerateFromDatabase;
        event EventHandler GeneratrForSelected;

        event EventHandler TitleNameTextBoxChanged;
        event EventHandler ContentNameTextBoxChanged;
        event EventHandler TagsNameTextBoxChanged;
        event EventHandler MetaTitleNameTextBoxChanged;
        event EventHandler MetaDescriptionNameTextBoxChanged;

        event EventHandler PromptFormatTextBoxChanged;
        event EventHandler PromptTextBoxChanged;
        event EventHandler TitleTextBoxChanged;
        event EventHandler ContentTextBoxChanged;
        event EventHandler TagsTextBoxChanged;

        // Methods
        void EnableUI();
        void DisableUI();
    }
}
