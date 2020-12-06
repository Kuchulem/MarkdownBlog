using Kuchulem.MarkdownBlog.Models;
using Kuchulem.MarkdownBlog.Services.CacheProvider;
using Kuchulem.MarkdownBlog.Services.Configurations;
using Kuchulem.MarkdownBlog.Services.MdFileParserServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kuchulem.MarkdownBlog.Services
{
    /// <summary>
    /// Service providing pages based on md files
    /// </summary>
    public class PageService : FileModelServiceBase<Page, PageData>
    {
        private const string PagesSubdirectory = "Pages";
        private const string PagesFileExtension = "md";
        private const string AllPagesQueryPart = "blog:all";
        private const string MenuPagesQueryPart = "blog:menu";
        private const string PagesQueryFormat = "pages:{0}";

        private readonly IFileModelCacheProvider<Page> cacheProvider;

        /// <summary>
        /// see <see cref="FileModelServiceBase{T, TData}.ModelSubDirectory"/>
        /// </summary>
        protected override string ModelSubDirectory => PagesSubdirectory;

        /// <summary>
        /// see <see cref="FileModelServiceBase{T, TData}.FileExtension"/>
        /// </summary>
        protected override string FileExtension => PagesFileExtension;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="cacheProvider"></param>
        /// <param name="fileParserService"></param>
        public PageService(IServicesConfiguration configuration, IFileModelCacheProvider<Page> cacheProvider, IMdFileParserService fileParserService)
            : base(configuration, fileParserService)
        {
            this.cacheProvider = cacheProvider;
        }

        /// <summary>
        /// Resets the cache
        /// </summary>
        public void ResetCache()
        {
            cacheProvider.ClearCache();
        }

        /// <summary>
        /// Gets all active pages
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Page> GetActivePages()
        {
            var query = string.Format(PagesQueryFormat, AllPagesQueryPart);
            var pages = cacheProvider.GetQuery(query);

            if (!pages.Any())
            {
                pages = GetAll().Where(p => p.Active);
                foreach (var page in pages)
                {
                    page.Author ??= configuration.DefaultAuthor;
                }

                cacheProvider.SetQuery(query, pages);
            }

            return pages;
        }

        /// <summary>
        /// Gets all pages displayed in the menu
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Gets a page from its slug
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        public Page GetPage(string slug)
        {
            return GetActivePages().Where(p => p.Slug == slug).FirstOrDefault();
        }
    }
}
