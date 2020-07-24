using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kuchulem.MarkdownBlog.Core.Models.Pages;
using Kuchulem.MarkdownBlog.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kuchulem.MarkdownBlog.Core.Controllers
{
    [Route("/")]
    public class PagesController : Controller
    {
        private readonly PageService pageService;

        public PagesController(PageService pageService)
        {
            this.pageService = pageService;
        }

        [HttpGet("{slug}")]
        public IActionResult Page(string slug)
        {
            var page = pageService.GetPage(slug);

            if (page is null)
                return NotFound(slug);

            return View(new PageViewModel
            {
                Description = page.Description,
                HtmlContent = page.HtmlContent,
                Slug = page.Slug,
                Tags = page.Tags,
                Title = page.Title
            });
        }
    }
}
