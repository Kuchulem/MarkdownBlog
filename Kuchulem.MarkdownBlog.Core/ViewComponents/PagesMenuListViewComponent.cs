using Kuchulem.MarkdownBlog.Core.Models.Pages;
using Kuchulem.MarkdownBlog.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kuchulem.MarkdownBlog.Core.ViewComponents
{
    /// <summary>
    /// Component to create a menu
    /// </summary>
    [ViewComponent]
    public class PagesMenuListViewComponent : ViewComponent
    {
        private readonly PageService pageService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pageService"></param>
        public PagesMenuListViewComponent(PageService pageService)
        {
            this.pageService = pageService;
        }

        /// <summary>
        /// Creates the view for the menu
        /// </summary>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var list = await Task.Run<IEnumerable<PagesMenuListItemViewModel>>(() =>
            {
                var list = new List<PagesMenuListItemViewModel>();

                var pages = pageService.GetMenuPages();

                foreach (var page in pages)
                {
                    list.Add(new PagesMenuListItemViewModel
                    {
                        Slug = page.Slug,
                        Title = page.Title
                    });
                }

                return list;
            });

            return View(new PageMenuListViewModel
            {
                Pages = list
            });
        }
    }
}
