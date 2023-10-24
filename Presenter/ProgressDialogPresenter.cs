using GPTArticleGen.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPTArticleGen.Presenter
{
    public class ProgressDialogPresenter
    {
        private IProgressDialogView _view;
        private int _imagesMax;
        private int _imagesCurrent;
        private int _articleMax;
        private int _articleCurrent;
        private int _pageMax;
        private int _pageCurrent;

        private string _initializationText;
        private string _addImagesDoneText;
        private string _generateArticleDoneText;
        private string _addToPageDoneText;
        private string _finizationText;

        public ProgressDialogPresenter(IProgressDialogView view, int imagesMax, int articleMax, int pageMax)
        {
            _view = view;

            _imagesMax = imagesMax;
            _articleMax = articleMax; 
            _pageMax = pageMax;
            _view.ProgressClick += ProgressClick;
        }

        private void ProgressClick(object? sender, EventArgs e)
        {
            Debug.WriteLine("ProgressClick");
            ChangeProgressText();
        }

        public void Initialize()
        {
            _view.AddImagesPrompt = "Adding images [n]/[m]\n";
            _view.GenerateArticlePrompt = "Generating article [n]/[m]\n";
            _view.AddToPagePrompt = "Adding article to page [n]/[m]\n";

            _view.AddImagesText = "";
            _view.GenerateArticleText = "";
            _view.AddToPageText = "";

            _initializationText = "Initializing...\n";
            _addImagesDoneText = "Images added.\n";
            _generateArticleDoneText = "Article generated.\n";
            _addToPageDoneText = "Article added to page.\n";
            _finizationText = "Done.";

            ChangeProgressText();
        }

        public void UpdateAddImagesProgress(int imagesCurrent)
        {
            _view.AddImagesText = _view.AddImagesPrompt.Replace("[n]", imagesCurrent.ToString()).Replace("[m]", _imagesMax.ToString());

            if(imagesCurrent == _imagesMax)
                _view.AddImagesText += _addImagesDoneText;
            ChangeProgressText();
        }

        public void UpdateGenerateArticleProgress(int articleCurrent)
        {
            _view.GenerateArticleText = _view.GenerateArticlePrompt.Replace("[n]", articleCurrent.ToString()).Replace("[m]", _articleMax.ToString());
            if (articleCurrent == _articleMax)
                _view.GenerateArticleText += _generateArticleDoneText;
            ChangeProgressText();
        }

        public void UpdateAddToPageProgress(int pageCurrent)
        {
            _view.AddToPageText = _view.AddToPagePrompt.Replace("[n]", pageCurrent.ToString()).Replace("[m]", _pageMax.ToString());
            if (pageCurrent == _pageMax)
                _view.AddToPageText += _addToPageDoneText;
            ChangeProgressText();
        }

        private void ChangeProgressText()
        {
            Task.Run(async () =>
            {
                // Update the UI with the result on the UI thread
                Program.SyncContext.Post(async _ =>
                {
                    _view.ProgressText = _initializationText + _view.AddImagesText + _view.GenerateArticleText + _view.AddToPageText;
                    if (_view.AddToPageText.Contains(_addToPageDoneText))
                        _view.ProgressText += _finizationText;
                }, null);
            });
        }
    }
}
