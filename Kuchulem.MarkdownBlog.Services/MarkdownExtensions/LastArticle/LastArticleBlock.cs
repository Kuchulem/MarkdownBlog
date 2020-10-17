using Markdig.Parsers;
using Markdig.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kuchulem.MarkdownBlog.Services.MarkdownExtensions.LastArticle
{
    public class LastArticleBlock : LeafBlock
    {
        public LastArticleBlock(BlockParser parser) : base(parser)
        {
            ProcessInlines = false;
        }
    }
}
