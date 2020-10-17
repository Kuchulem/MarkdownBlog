using Kuchulem.MarkdownBlog.Libs.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Kuchulem.MarkdownBlog.Models
{
    /// <summary>
    /// Model for pages
    /// </summary>
    public class Page : IFileModel
    {

        #region IFileModel
        /// <summary>
        /// see <see cref="IFileModel.Name"/>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// see <see cref="IFileModel.Extension"/>
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// see <see cref="IFileModel.RawContent"/>
        /// </summary>
        public string RawContent { get; set; }

        /// <summary>
        /// see <see cref="IFileModel.Title"/>
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// see <see cref="IFileModel.Tags"/>
        /// </summary>
        public IEnumerable<string> Tags { get; set; }

        /// <summary>
        /// see <see cref="IFileModel.IsValid"/>
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// see <see cref="IFileModel.HtmlContent"/>
        /// </summary>
        public string HtmlContent { get; set; }
        #endregion

        /// <summary>
        /// Description of the page
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Slug of the page (titled formed for unique url)
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// Wether the page is provided in the blog
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Wether the page is displayed in the blog's header menu
        /// </summary>
        public bool Menu { get; set; }
    }
}
