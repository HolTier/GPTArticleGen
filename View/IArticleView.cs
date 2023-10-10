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
        ArticleModel SelectedTitle { get; set; }
        WebView2 WebView2 { get; set; }
        Control UiControl { get; }
        BindingList<ArticleModel> Titles { get; set; }

        // Events
        event EventHandler GenerateArticle;
        event EventHandler GenerateForAll;
        event EventHandler ChangeDefaultPrompt;
        event EventHandler ImportTitles;
        event EventHandler AddToPageAsync;
        event EventHandler RegenarateArticle;
        event EventHandler SelectedTitleChanged;

        // Methods
        void NavigateToPage(string url);
    }
}
