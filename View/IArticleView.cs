﻿using Microsoft.Web.WebView2.WinForms;
using System;
using System.Collections.Generic;
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
        string[] Tags { get; set; }
        string Prompt { get; set; }

        WebView2 WebView2 { get; set; }
        Control UiControl { get; }

        // Events
        event EventHandler GenerateArticle;
        event EventHandler GenerateForAll;
        event EventHandler ChangeDefaultPrompt;
        event EventHandler ImportTitles;
        event EventHandler AddToPage;

        // Methods
        void NavigateToPage(string url);
    }
}
