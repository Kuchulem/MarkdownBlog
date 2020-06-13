using Kuchulem.MarkDownBlog.Client.Models;
using Kuchulem.MarkDownBlog.Client.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
            page = page < 1 ? 1 : page;
            count = count <= 0 ? 20 : count;

            return Ok(articleService.GetLastArticles(page, count));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        public ActionResult<Article> Get(string slug)
        {

        }
    }
}
