using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kuchulem.MarkdownBlog.Models
{
    /// <summary>
    /// Interface describing a MD file based model
    /// </summary>
    public interface IFileModel
    {
        /// <summary>
        /// Wether the file model has valid content
        /// </summary>
        bool IsValid { get; set; }

        /// <summary>
        /// The slug of the file (its Url friendly name) used for the resulting file name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The human readable title of the md file (first `#` tag)
        /// </summary>
        string Title { get; set; }
        
        /// <summary>
        /// The file extension
        /// </summary>
        string Extension { get; set; }

        /// <summary>
        /// The raw content of the file
        /// </summary>
        string RawContent { get; set; }

        /// <summary>
        /// A one paragraph summary of the file
        /// </summary>
        string Summary { get; set; }

        /// <summary>
        /// The html content of the file
        /// </summary>
        string HtmlContent { get; set; }

        /// <summary>
        /// Tags for the file
        /// </summary>
        IEnumerable<string> Tags { get; set; }
    }
}
