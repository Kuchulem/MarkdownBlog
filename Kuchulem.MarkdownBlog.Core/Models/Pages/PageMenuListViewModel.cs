using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kuchulem.MarkdownBlog.Core.Models.Pages
{
    public class PageMenuListViewModel
    {
        public IEnumerable<PagesMenuListItemViewModel> Pages { get; set; }
    }
}
