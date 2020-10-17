using Markdig.Parsers;
using Markdig.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kuchulem.MarkdownBlog.Services.MarkdownExtensions.LastArticle
{
    public class LastArticleParser : TechnicalBlockParser
    {
        private const string Tag = "LastArticle";

        public LastArticleParser() : base(Tag)
        {
        }

        protected override LeafBlock MakeBlock(IEnumerable<string> blockParams)
        {
            return new LastArticleBlock(this);
        }
    }
}
