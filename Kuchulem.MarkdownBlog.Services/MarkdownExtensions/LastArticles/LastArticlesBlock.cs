using Markdig.Parsers;
using Markdig.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kuchulem.MarkdownBlog.Services.MarkdownExtensions.LastArticles
{
    public class LastArticlesBlock : LeafBlock
    {
        public LastArticlesBlock(BlockParser parser) : base(parser)
        {
            ProcessInlines = false;
        }

        public int NbArticles { get; set; }
    }
}
