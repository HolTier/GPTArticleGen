using GPTArticleGen.Model;
using GPTArticleGen.View;
using System;
using System.Collections.Generic;
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
            _view.Content = _view.Prompt;
            _model.Content = _view.Content;
            _model.Prompt = _view.Prompt;
        }
    }
}
