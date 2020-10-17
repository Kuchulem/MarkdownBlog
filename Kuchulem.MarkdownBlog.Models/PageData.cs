using System;
using System.Collections.Generic;
using System.Text;

namespace Kuchulem.MarkdownBlog.Models
{
    /// <summary>
    /// Data for page
    /// </summary>
    public class PageData
    {
        /// <summary>
        /// Slug (URL friendly slug)
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// Tags for the page
        /// </summary>
        public IEnumerable<string> Tags { get; set; }

        /// <summary>
        /// Wether the page is available on the blog
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Wether the page is displayed in the menu
        /// </summary>
        public bool Menu { get; set; }

        /// <summary>
        /// Description of the page
        /// </summary>
        public string Description { get; set; }
    }
}
