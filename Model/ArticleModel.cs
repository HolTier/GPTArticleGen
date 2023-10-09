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
        public string Title { get; set; }
        public string Content { get; set; }
        public BindingList<string> Tags { get; set; }
        public string Prompt { get; set; }

    }
}
