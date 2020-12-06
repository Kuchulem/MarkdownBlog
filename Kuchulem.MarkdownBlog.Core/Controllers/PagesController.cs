using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kuchulem.MarkdownBlog.Core.Models.Pages;
using Kuchulem.MarkdownBlog.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kuchulem.MarkdownBlog.Core.Controllers
{
    /// <summary>
    /// Controller for pages
    /// </summary>
    [Route("/")]
    public class PagesController : Controller
    {
        private readonly PageService pageService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pageService"></param>
        public PagesController(PageService pageService)
        {
            this.pageService = pageService;
        }

        /// <summary>
        /// Action to get a page from its slug
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        [HttpGet("{slug}")]
        public IActionResult Page(string slug)
        {
            var page = pageService.GetPage(slug);

            if (page is null)
                return NotFound(slug);

            return View(nameof(Page), new PageViewModel
            {
                Summary = page.Summary,
                HtmlContent = page.HtmlContent,
                Slug = page.Slug,
                Tags = page.Tags,
                Title = page.Title,
                Author = page.Author
            });
        }

        /// <summary>
        /// Single article page
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        [HttpGet("{category}/{slug}")]
        public IActionResult PageFromCategory(string category, string slug)
        {
            if (string.IsNullOrEmpty(category))
                return BadRequest();

            var page = pageService.GetPage(slug);

            if (page is null)
                return NotFound(slug);

            return View(nameof(Page), new PageViewModel
            {
                Summary = page.Summary,
                HtmlContent = page.HtmlContent,
                Slug = page.Slug,
                Tags = page.Tags,
                Title = page.Title,
                Author = page.Author
            });
        }
    }
}
