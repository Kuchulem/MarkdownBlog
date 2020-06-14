using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Kuchulem.MarkDownBlog.Client.Configuration
{
    public class ApplicationConfiguration
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
