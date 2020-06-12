using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kuchulem.MarkDownBlog.Api.Models
{
    public abstract class FileModelBase
    {
        public abstract string Path { get; }

        public string FileName { get; set; }

        public string Content { get; set; }
    }
}
