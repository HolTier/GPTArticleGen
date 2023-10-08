﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPTArticleGen.Model
{
    public class ArticleModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string[] Tags { get; set; }
        public string Prompt { get; set; }

    }
}
