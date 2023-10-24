using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPTArticleGen.Model
{
    public class ArticleDatabaseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Tags { get; set; }
        public string Prompt { get; set; }
        public bool IsPublished { get; set; }
        public int ImageId { get; set; }
        public int PageId { get; set; }
    }
}
