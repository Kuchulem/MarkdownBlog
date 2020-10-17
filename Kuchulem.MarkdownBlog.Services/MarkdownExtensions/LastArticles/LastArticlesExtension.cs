using Markdig;
using Markdig.Renderers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kuchulem.MarkdownBlog.Services.MarkdownExtensions.LastArticles
{
    class LastArticlesExtension : IMarkdownExtension
    {
        private readonly LastArticlesOptions options;

        public LastArticlesExtension(LastArticlesOptions options)
        {
            this.options = options;
        }
        public void Setup(MarkdownPipelineBuilder pipeline)
        {
            var parsers = pipeline.BlockParsers;

            if (!parsers.Contains<LastArticlesParser>())
                parsers.Add(new LastArticlesParser());
        }

        public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
        {
            var renderers = (renderer as HtmlRenderer)?.ObjectRenderers;

            if (renderers != null && !renderers.Contains<LastArticlesRenderer>())
                renderers.Add(new LastArticlesRenderer(options));
        }
    }
}
