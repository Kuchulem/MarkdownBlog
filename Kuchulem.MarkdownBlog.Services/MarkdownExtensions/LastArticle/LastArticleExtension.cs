using Kuchulem.MarkdownBlog.Services.MarkdownExtensions.LastArticles;
using Markdig;
using Markdig.Renderers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kuchulem.MarkdownBlog.Services.MarkdownExtensions.LastArticle
{
    class LastArticleExtension : IMarkdownExtension
    {
        private readonly LastArticlesOptions options;

        public LastArticleExtension(LastArticlesOptions options)
        {
            this.options = options;
        }
        public void Setup(MarkdownPipelineBuilder pipeline)
        {
            var parsers = pipeline.BlockParsers;

            if (!parsers.Contains<LastArticleParser>())
                parsers.Add(new LastArticleParser());
        }

        public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
        {
            var renderers = (renderer as HtmlRenderer)?.ObjectRenderers;

            if (renderers != null && !renderers.Contains<LastArticleRenderer>())
                renderers.Add(new LastArticleRenderer(options));
        }
    }
}
