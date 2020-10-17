using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kuchulem.MarkdownBlog.Core.Models.Articles
{
    /// <summary>
    /// View model for article summary
    /// </summary>
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

        /// <summary>
        /// The main pic of the article
        /// </summary>
        public string MainPicture { get; set; }

        /// <summary>
        /// Credits fot the picture
        /// </summary>
        public string MainPictureCredits { get; set; }

        /// <summary>
        /// Summary of the article
        /// </summary>
        public string Summary { get; set; }


        /// <summary>
        /// Last publication date
        /// </summary>
        public DateTime PublicationDate { get; set; }

        /// <summary>
        /// Author of the article
        /// </summary>
        public string Author { get; set; }
    }
}
