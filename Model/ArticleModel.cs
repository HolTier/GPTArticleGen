using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPTArticleGen.Model
{
    public class ArticleModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Tags { get; set; }
        public string Prompt { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public bool IsPublished { get; set; }
        public int SiteId { get; set; }
        public string ImagePath { get; set; }
        public int ImageId { get; set; }
        public string PromptTitle { get; set; }
        public string PromptFormat { get; set; }
        public string RawData { get; set; }
        public string PostData { get; set; }
        public int Retries { get; set; } = 1;
        public int PageId { get; set; }

        // Page
        public string Site { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string TitleName { get; set; }
        public string ContentName { get; set; }
        public string TagsName { get; set; }
        public string MetaTitleName { get; set; }
        public string MetaDescriptionName { get; set; }
    }
}
