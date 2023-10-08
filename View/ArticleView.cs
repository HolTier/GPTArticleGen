using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPTArticleGen.View
{
    public partial class ArticleView : Form, IArticleView
    {
        private string _title;
        private string[] _tags;
        public ArticleView()
        {
            InitializeComponent();
            generateButton.Click += (sender, args) => GenerateArticle?.Invoke(this, EventArgs.Empty);
            generateAllButton.Click += (sender, args) => GenerateForAll?.Invoke(this, EventArgs.Empty);
            changePromptButton.Click += (sender, args) => ChangeDefaultPrompt?.Invoke(this, EventArgs.Empty);
            importTitlesButton.Click += (sender, args) => ImportTitles?.Invoke(this, EventArgs.Empty);
            addToPageButton.Click += (sender, args) => AddToPage?.Invoke(this, EventArgs.Empty);
        }


        public string Title
        {
            get => _title;
            set => _title = value;
        }
        public string Content
        {
            get => content.Text;
            set => content.Text = value;
        }
        public string[] Tags
        {
            get => _tags;
            set => _tags = value;
        }
        public string Prompt
        {
            get => prompt.Text;
            set => prompt.Text = value;
        }

        public event EventHandler GenerateArticle;
        public event EventHandler GenerateForAll;
        public event EventHandler ChangeDefaultPrompt;
        public event EventHandler ImportTitles;
        public event EventHandler AddToPage;
    }
}
