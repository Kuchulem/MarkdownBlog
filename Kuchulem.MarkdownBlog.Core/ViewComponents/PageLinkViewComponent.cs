using Kuchulem.MarkdownBlog.Core.Models.Pages;
using Kuchulem.MarkdownBlog.Models;
using Kuchulem.MarkdownBlog.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kuchulem.MarkdownBlog.Core.ViewComponents
{
    [ViewComponent]
    public class PageLinkViewComponent : ViewComponent
    {
        private readonly PageService pageService;

        public PageLinkViewComponent(PageService pageService)
        {
            this.pageService = pageService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string slug)
        {
            var page = await Task.Run(() => pageService.GetPage(slug));

            var viewModel = page != null ? new PagesMenuListItemViewModel
            {
                Slug = page.Slug,
                Title = page.Title
            } : null;

            return View(viewModel);
        }
    }
}
