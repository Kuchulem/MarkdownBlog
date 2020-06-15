using Kuchulem.MarkDownBlog.Libs.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Kuchulem.MarkDownBlog.Models;
using Kuchulem.MarkDownBlog.Services;
using Microsoft.AspNetCore.Mvc.Routing;
using Kuchulem.MarkdownBlog.Core.Models.Articles;

namespace Kuchulem.MarkDownBlog.Core.Controllers
{
    [Route("{Controller}")]
    public class ArticlesController : Controller
    {
        private const int PageDefault = 1;
        private const int CountDefault = 20;
        private readonly ArticleService articleService;

        public ArticlesController(ArticleService articleService)
        {
            this.articleService = articleService;
        }

        public IActionResult Index(int? page = null, int? count = null)
        {
#if DEBUG
            this.WriteDebugLine(message: "------------------------------------------");
            this.WriteDebugLine(message: $"page {page} - count {count}");
#endif
            if (page < 1 || count <= 0)
                return RedirectToAction(nameof(Index));

            if (page == 1 && count == 20)
                return RedirectToActionPermanent(nameof(Index));

            var actualPage = page ?? PageDefault;
            var actualCount = count ?? CountDefault;

            var list = articleService.GetLastArticles(actualPage, actualCount).Select(a => new ArticleSummaryViewModel
            {
                PublicationDate = a.PublicationDate,
                Slug = a.Slug,
                Summary = a.Summary,
                Tags = a.Tags,
                MainPicture = a.MainPicture,
                Title = a.Title
            }).ToList();

            var totalCount = articleService.GetCountReadableArticles();

            return View(new ArticleListViewModel
            {
                CountByPage = actualCount,
                Elements = list,
                Page = actualPage,
                TotalCount = totalCount,
                Url = HttpContext.Request.Path.Value
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        [HttpGet("{slug}")]
        public ActionResult<Article> Article(string slug)
        {
            if (string.IsNullOrEmpty(slug))
                return BadRequest();

            var article = articleService.GetArticle(slug);

            if (article is null)
                return NotFound(slug);

            return View(new ArticleViewModel
            {
                HtmlContent = article.HtmlContent,
                MainPicture = article.MainPicture,
                PublicationDate = article.PublicationDate,
                Slug = article.Slug,
                Summary = article.Summary,
                Tags = article.Tags,
                Title = article.Title
            });
        }
    }
}
