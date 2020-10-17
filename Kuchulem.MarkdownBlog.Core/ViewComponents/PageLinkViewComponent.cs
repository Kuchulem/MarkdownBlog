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
    /// <summary>
    /// View component for page link
    /// </summary>
    [ViewComponent]
    public class PageLinkViewComponent : ViewComponent
    {
        private readonly PageService pageService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pageService"></param>
        public PageLinkViewComponent(PageService pageService)
        {
            this.pageService = pageService;
        }

        /// <summary>
        /// Creats a page menu item view model
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
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
