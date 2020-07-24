﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Kuchulem.MarkdownBlog.Core.Models;
using Kuchulem.MarkdownBlog.Core.Models.Pages;
using Kuchulem.MarkdownBlog.Services;

namespace Kuchulem.MarkdownBlog.Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly PageService pageService;
        private readonly ILogger<HomeController> logger;

        public HomeController(PageService pageService, ILogger<HomeController> logger)
        {
            this.pageService = pageService;
            this.logger = logger;
        }

        public IActionResult Index()
        {
            var page = pageService.GetPage("home");

            if (page is null)
            {
                logger.LogWarning("No home page");
                return View(new PageViewModel
                {
                    Title = "Home"
                });
            }

            return View(new PageViewModel
            {
                Description = page.Description,
                HtmlContent = page.HtmlContent,
                Slug = page.Slug,
                Tags = page.Tags,
                Title = page.Title
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
