using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPTArticleGen.View
{
    public partial class ProgressDialogView : Form, IProgressDialogView
    {
        private string _progressText;
        private string _addImagesText;
        private string _addImagesPrompt;
        private string _generateArticleText;
        private string _generateArticlePrompt;
        private string _addToPageText;
        private string _addToPagePrompt;

        public ProgressDialogView()
        {
            InitializeComponent();
            //progressLabel.Click += (sender, args) => ProgressClick?.Invoke(this, EventArgs.Empty);
        }

        public string AddImagesText
        {
            get => _addImagesText;
            set => _addImagesText = value;
        }
        public string AddImagesPrompt
        {
            get => _addImagesPrompt;
            set => _addImagesPrompt = value;
        }
        public string GenerateArticleText
        {
            get => _generateArticleText;
            set => _generateArticleText = value;
        }
        public string GenerateArticlePrompt
        {
            get => _generateArticlePrompt;
            set => _generateArticlePrompt = value;
        }
        public string AddToPageText
        {
            get => _addToPageText;
            set => _addToPageText = value;
        }
        public string AddToPagePrompt
        {
            get => _addToPagePrompt;
            set => _addToPagePrompt = value;
        }
        public string ProgressText
        {
            get => progressLabel.Text;
            set
            {
                progressLabel.Text = value;
            }
        }

        public event EventHandler ProgressClick;

       
    }
}
