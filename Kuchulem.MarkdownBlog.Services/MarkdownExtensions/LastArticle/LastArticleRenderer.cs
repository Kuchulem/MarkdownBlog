using Kuchulem.MarkdownBlog.Services.MarkdownExtensions.LastArticle;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Kuchulem.MarkdownBlog.Services.MarkdownExtensions.LastArticles
{
    public class LastArticleRenderer : HtmlObjectRenderer<LastArticleBlock>
    {
        private readonly LastArticlesOptions options;

        public LastArticleRenderer(LastArticlesOptions options)
        {
            this.options = options;
        }

        protected override void Write(HtmlRenderer renderer, LastArticleBlock obj)
        {
            var articles = options.ArticleService.GetLastArticles(1, 1);
            renderer.Write("<div class=\"card-container\">");
            foreach (var article in articles)
            {
                renderer.Write(options.ArticleRenderer.Invoke(article));
                //var url = string.Format(options.ArticleUrlFormat, article.Slug);

                //var publicationDate = article.PublicationDate.ToString("D", CultureInfo.CurrentCulture);
                //renderer
                //    .Write("<section class=\"article card\">\n")
                //    .Write($"    <div class=\"card-picture bottom-shadow-3d\" style=\"background-image:url({article.Picture.Main})\">\n")
                //    .Write($"        <h2>{article.Title}</h2>\n")
                //    .Write("    </div >\n")
                //    .Write("    <div class=\"card-content\">\n")
                //    .Write("        <div class=\"publication\" >\n")
                //    .Write($"            <span>published on {publicationDate}</span> by<a class=\"author\">{article.Author ?? options.DefaultAuthor}</a>\n")
                //    .Write("        </div>\n")
                //    .Write($"        {article.Summary}\n")
                //    .Write("    </div>\n")
                //    .Write($"    <a href=\"{url}\">Lire</a>\n")
                //    .Write("</section>\n");
            }
            renderer.Write("</div>");
        }
    }
}
