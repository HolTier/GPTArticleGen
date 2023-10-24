using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPTArticleGen.View
{
    public interface IProgressDialogView
    {
        public string ProgressText { get; set; }
        public string AddImagesText { get; set; }
        public string AddImagesPrompt { get; set; }
        public string GenerateArticleText { get; set; }
        public string GenerateArticlePrompt { get; set; }
        public string AddToPageText { get; set; }
        public string AddToPagePrompt { get; set; }

        event EventHandler ProgressClick;
    }
}
