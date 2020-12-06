using System;
using System.Collections.Generic;

namespace Kuchulem.MarkdownBlog.Models
{
    /// <summary>
    /// An article
    /// </summary>
    public class Article : IFileModel
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
        /// Slug of the article (titled formed for unique url)
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// Picture of the article
        /// </summary>
        public ArticlePicture Picture { get; set; }

        /// <summary>
        /// Date of publication of the article
        /// </summary>
        public DateTime PublicationDate { get; set; }

        /// <summary>
        /// Short summary of the article displayed in thumbnails and just bellow the title
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Author of the article
        /// </summary>
        public string Author { get; set; }
    }
}
