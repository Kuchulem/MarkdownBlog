using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kuchulem.MarkdownBlog.Models
{
    /// <summary>
    /// Data for an article
    /// </summary>
    public class ArticleData
    {
        /// <summary>
        /// Slug (URL friendly title)
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// Tags for the article
        /// </summary>
        public IEnumerable<string> Tags { get; set; }

        /// <summary>
        /// Picture of the article
        /// </summary>
        public ArticlePicture Picture { get; set; }

        /// <summary>
        /// Date of publication of the article
        /// </summary>
        public DateTime PublicationDate { get; set; }

        /// <summary>
        /// Author of the article
        /// </summary>
        public string Author { get; set; }
    }
}
