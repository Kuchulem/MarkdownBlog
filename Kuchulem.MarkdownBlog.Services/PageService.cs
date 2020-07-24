using Kuchulem.MarkdownBlog.Models;
using Kuchulem.MarkdownBlog.Services.CacheProvider;
using Kuchulem.MarkdownBlog.Services.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kuchulem.MarkdownBlog.Services
{
    public class PageService : FileModelServiceBase<Page>
    {
        private const string PagesSubdirectory = "Pages";
        private const string PagesFileExtension = "md";
        private const string AllPagesQueryPart = "blog:all";
        private const string MenuPagesQueryPart = "blog:menu";
        private const string PagesQueryFormat = "pages:{0}";

        private readonly IFileModelCacheProvider<Page> cacheProvider;

        protected override string ModelSubDirectory => PagesSubdirectory;

        protected override string FileExtension => PagesFileExtension;

        public PageService(IServicesConfiguration configuration, IFileModelCacheProvider<Page> cacheProvider)
            : base(configuration)
        {
            this.cacheProvider = cacheProvider;
        }

        public void ResetCache()
        {
            cacheProvider.ResetCache();
        }

        public IEnumerable<Page> GetActivePages()
        {
            var query = string.Format(PagesQueryFormat, AllPagesQueryPart);
            var pages = cacheProvider.GetQuery(query);

            if (!pages.Any())
            {
                pages = GetAll().Where(p => p.Active);

                cacheProvider.SetQuery(query, pages);
            }

            return pages;
        }

        public IEnumerable<Page> GetMenuPages()
        {
            var query = string.Format(PagesQueryFormat, MenuPagesQueryPart);
            var pages = cacheProvider.GetQuery(query);

            if (!pages.Any())
            {
                pages = GetActivePages().Where(p => p.Menu);

                cacheProvider.SetQuery(query, pages);
            }

            return pages;
        }

        public Page GetPage(string slug)
        {
            return GetActivePages().Where(p => p.Slug == slug).FirstOrDefault();
        }
    }
}
