using System;
using System.Collections.Generic;
using System.Text;

namespace Kuchulem.MarkdownBlog.Services.Configurations
{
    /// <summary>
    /// Interface describing the configuration of the services
    /// </summary>
    public interface IServicesConfiguration
    {
        /// <summary>
        /// Path to the files to store
        /// </summary>
        string DataFilesPath { get; }

        /// <summary>
        /// Default author
        /// </summary>
        string DefaultAuthor { get; set; }
    }
}
