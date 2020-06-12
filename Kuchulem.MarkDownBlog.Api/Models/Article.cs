using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Kuchulem.MarkDownBlog.Api.Models
{
    public class Article : FileModelBase
    {
        private const string DirectoryPath = "Articles";

        public override string Path => DirectoryPath;
    }
}
