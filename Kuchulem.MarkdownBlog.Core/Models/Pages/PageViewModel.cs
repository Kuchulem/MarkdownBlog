using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kuchulem.MarkdownBlog.Core.Models.Pages
{
    public class PageViewModel
    {
        public string Title { get; set; }
        public string HtmlContent { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> Tags { get; set; } = Enumerable.Empty<string>();
        public string Slug { get; set; }
    }
}
