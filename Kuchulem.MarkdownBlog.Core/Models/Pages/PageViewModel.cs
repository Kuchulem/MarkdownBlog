using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kuchulem.MarkdownBlog.Core.Models.Pages
{
    /// <summary>
    /// View model for page
    /// </summary>
    public class PageViewModel : IPageViewModel
    {
        /// <summary>
        /// Title pf the page
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Html content of the page
        /// </summary>
        public string HtmlContent { get; set; }

        /// <summary>
        /// Description of the page
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Tags for the page
        /// </summary>
        public IEnumerable<string> Tags { get; set; } = Enumerable.Empty<string>();

        /// <summary>
        /// Slug (URL-friendly title) for the page
        /// </summary>
        public string Slug { get; set; }
    }
}
