using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPTArticleGen.Model
{
    public class PageModel
    {
        public int Id { get; set; }
        public string site { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
