using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kuchulem.MarkdownBlog.Core.Models.Pages
{
    /// <summary>
    /// View model for page menu item
    /// </summary>
    public class PagesMenuListItemViewModel
    {
        /// <summary>
        /// Title of the item
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Slug of the item
        /// </summary>
        public string Slug { get; set; }
    }
}
