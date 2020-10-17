using Kuchulem.MarkdownBlog.Services.Configurations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Kuchulem.MarkdownBlog.Core.Configuration
{
    public class ApplicationConfiguration : IServicesConfiguration
    {
        public string DataFilesPath { get; set; }

        public string DefaultAuthor { get; set; }

        public bool IsComplete
        {
            get
            {
                return 
                    !string.IsNullOrEmpty(DataFilesPath)
                    && !string.IsNullOrEmpty(DefaultAuthor);
            }
        }
    }
}
