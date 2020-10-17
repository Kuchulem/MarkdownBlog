using System;

namespace Kuchulem.MarkdownBlog.Core.Models
{
    /// <summary>
    /// View model for Errors
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Id of the request providing the error
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// Wether the request id can be shown
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
