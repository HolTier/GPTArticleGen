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
        public bool IsPublished { get; set; }
        public string PromptTitle { get; set; }
        public string PromptFormat { get; set; }
        public string RawData { get; set; }
        public string PostData { get; set; }
        public int Retries { get; set; } = 1;
        
    }
}
