using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kuchulem.MarkdownBlog.Core.Models.Articles
{
    public class ArticleSummaryViewModel
    {
        /// <summary>
        /// Slug (URL friendly) string of the article
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The tags for the article
        /// </summary>
        public IEnumerable<string> Tags { get; set; }

        public string Thumbnail { get; set; }

        public string Summary { get; set; }
    }
}
