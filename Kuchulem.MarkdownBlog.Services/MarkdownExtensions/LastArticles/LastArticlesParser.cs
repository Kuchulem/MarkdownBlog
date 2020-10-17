using Markdig.Parsers;
using Markdig.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kuchulem.MarkdownBlog.Services.MarkdownExtensions.LastArticles
{
    public class LastArticlesParser : TechnicalBlockParser
    {
        private const string Tag = "LastArticles";
        private const int DefaultNumberOfArticles = 5;

        public LastArticlesParser() : base(Tag)
        {
        }

        protected override LeafBlock MakeBlock(IEnumerable<string> blockParams)
        {
            if(!int.TryParse(blockParams.FirstOrDefault(), out int nbArticles))
                nbArticles = DefaultNumberOfArticles;

            return new LastArticlesBlock(this)
            {
                NbArticles = nbArticles
            };
        }
    }
}
