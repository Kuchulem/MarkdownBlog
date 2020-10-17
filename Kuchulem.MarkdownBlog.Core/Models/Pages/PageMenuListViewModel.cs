using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kuchulem.MarkdownBlog.Core.Models.Pages
{
    /// <summary>
    /// View model for page menu
    /// </summary>
    public class PageMenuListViewModel
    {
        /// <summary>
        /// Pages in the menu
        /// </summary>
        public IEnumerable<PagesMenuListItemViewModel> Pages { get; set; }
    }
}
