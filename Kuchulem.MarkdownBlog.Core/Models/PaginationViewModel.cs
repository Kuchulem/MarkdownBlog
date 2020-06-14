using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kuchulem.MarkdownBlog.Core.Models
{
    public abstract class PaginationViewModel<T>
    {
        public abstract string Title { get; }
        public int Page { get; set; }
        public int CountByPage { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<T> Elements { get; set; }
        public string Url { get; set; }
        public string PreviousUrl
        {
            get
            {
                if (Page <= 1)
                    return null;

                return $"{Url}?page={Page}&count={CountByPage}";
            }
        }
        public string NextUrl
        {
            get
            {
                if ((Page + 1) * CountByPage > TotalCount)
                    return null;

                return $"{Url}?page={Page + 1}&count={CountByPage}";
            }
        }
    }
}
