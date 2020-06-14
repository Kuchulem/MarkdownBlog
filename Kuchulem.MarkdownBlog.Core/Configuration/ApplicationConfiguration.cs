using Kuchulem.MarkdownBlog.Services.Configurations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Kuchulem.MarkDownBlog.Core.Configuration
{
    public class ApplicationConfiguration : IServicesConfiguration
    {
        public string DataFilesPath { get; set; }

        public bool IsComplete
        {
            get
            {
                return !string.IsNullOrEmpty(DataFilesPath);
            }
        }
    }
}
