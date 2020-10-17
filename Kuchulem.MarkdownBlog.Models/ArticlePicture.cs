using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kuchulem.MarkdownBlog.Models
{
    /// <summary>
    /// Picture of the article
    /// </summary>
    public class ArticlePicture
    {
        /// <summary>
        /// Full sized picture
        /// </summary>
        public string Main { get; set; }

        /// <summary>
        /// Thumbnail version
        /// </summary>
        public string Thumbnail { get; set; }

        /// <summary>
        /// Credits fot the picture
        /// </summary>
        public string Credits { get; set; }
    }
}
