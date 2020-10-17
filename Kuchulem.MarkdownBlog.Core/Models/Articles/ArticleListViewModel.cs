using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kuchulem.MarkdownBlog.Core.Models.Articles
{
    /// <summary>
    /// View model for article list view
    /// </summary>
    public class ArticleListViewModel : PaginationViewModel<ArticleSummaryViewModel>
    {
        /// <summary>
        /// see <see cref="PaginationViewModel{T}.Title"/>
        /// </summary>
        public override string Title => "Last articles";
    }
}
