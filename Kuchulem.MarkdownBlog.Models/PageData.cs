using System;
using System.Collections.Generic;
using System.Text;

namespace Kuchulem.MarkdownBlog.Models
{
    public class PageData
    {
        public string Slug { get; set; }

        public IEnumerable<string> Tags { get; set; }

        public bool Active { get; set; }

        public bool Menu { get; set; }

        public string Description { get; set; }
    }
}
