using Kuchulem.MarkdownBlog.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace Kuchulem.MarkdownBlog.Services.MarkdownExtensions.LastArticles
{
    public class LastArticlesOptions
    {
        private readonly Func<ArticleService> serviceProvider;

        public LastArticlesOptions(Func<ArticleService> serviceProvider, Func<Article, string> articleRenderer)
        {
            this.serviceProvider = serviceProvider;
            ArticleRenderer = articleRenderer;
        }

        public ArticleService ArticleService => serviceProvider.Invoke();
        public string ArticleUrlFormat { get; set; }
        public string DefaultAuthor { get; set; }
        public Func<Article, string> ArticleRenderer { get; }
    }
}
