using Kuchulem.MarkdownBlog.Services.MarkdownExtensions.LastArticle;
using Kuchulem.MarkdownBlog.Services.MarkdownExtensions.LastArticles;
using Markdig;
using Markdig.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kuchulem.MarkdownBlog.Services.MarkdownExtensions
{
    public static class PipelineExtension
    {
        public static MarkdownPipelineBuilder UseLastArticles(this MarkdownPipelineBuilder pipeline, LastArticlesOptions options)
        {
            var extensions = pipeline.Extensions;

            if (!extensions.Contains<LastArticlesExtension>())
                extensions.Add(new LastArticlesExtension(options));

            return pipeline;
        }
        public static MarkdownPipelineBuilder UseLastArticle(this MarkdownPipelineBuilder pipeline, LastArticlesOptions options)
        {
            var extensions = pipeline.Extensions;

            if (!extensions.Contains<LastArticleExtension>())
                extensions.Add(new LastArticleExtension(options));

            return pipeline;
        }
    }
}
