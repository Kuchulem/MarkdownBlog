using Kuchulem.MarkDownBlog.Libs.Extensions;
using Kuchulem.MarkDownBlog.Client.Models;
using Kuchulem.MarkDownBlog.Client.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Kuchulem.MarkDownBlog.Client.Controllers
{
    [Route("api/{Controller}")]
    public class ArticlesController : Controller
    {
        private readonly ArticleService articleService;

        public ArticlesController(ArticleService articleService)
        {
            this.articleService = articleService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Article>> Get(int page, int count)
        {
#if DEBUG
            this.WriteDebugLine(message: "------------------------------------------");
            this.WriteDebugLine(message: $"page {page} - count {count}");
#endif
            page = page < 1 ? 1 : page;
            count = count <= 0 ? 20 : count;

            return Ok(articleService.GetLastArticles(page, count));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        [HttpGet("{slug}")]
        public ActionResult<Article> Get(string slug)
        {
            if (string.IsNullOrEmpty(slug))
                return BadRequest();

            var article = articleService.GetArticle(slug);

            if (article is null)
                return NotFound(slug);

            return Ok(article);
        }
    }
}
