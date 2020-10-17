using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kuchulem.MarkdownBlog.Core.Models
{
    /// <summary>
    /// Base class for view mmodel based on pagination
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class PaginationViewModel<T> : IPageViewModel
    {
        /// <summary>
        /// Title of the page
        /// </summary>
        public abstract string Title { get; }

        /// <summary>
        /// Page number in the pagination
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Number of elements in a page
        /// </summary>
        public int CountByPage { get; set; }

        /// <summary>
        /// Total count of elements
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Elements for the page
        /// </summary>
        public IEnumerable<T> Elements { get; set; }

        /// <summary>
        /// Url of the current page
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Url of the previous page (is any)
        /// </summary>
        public string PreviousUrl
        {
            get
            {
                if (Page <= 1)
                    return null;

                return $"{Url}?page={Page}&count={CountByPage}";
            }
        }

        /// <summary>
        /// Url of the next page (if any)
        /// </summary>
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
