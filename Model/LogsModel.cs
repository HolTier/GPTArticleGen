using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPTArticleGen.Model
{
    public class LogsModel
    {
        public int Id { get; set; }
        public string PromptTitle { get; set; }
        public string Site { get; set; }
        public int PostId { get; set; }
        public string PostUrl { get; set; }
        public string Username { get; set; }
        public DateTime Date { get; set; }
    }
}
