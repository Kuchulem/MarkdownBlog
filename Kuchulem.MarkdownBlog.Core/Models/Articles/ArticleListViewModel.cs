using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kuchulem.MarkdownBlog.Core.Models.Articles
{
    public class ArticleListViewModel : PaginationViewModel<ArticleSummaryViewModel>
    {
        public override string Title => "Les derniers articles";
    }
}
