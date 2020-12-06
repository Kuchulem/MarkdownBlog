using Kuchulem.MarkdownBlog.Services.Configurations;
using Kuchulem.MarkdownBlog.Models;
using Kuchulem.MarkdownBlog.Services.CacheProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using Kuchulem.MarkdownBlog.Services.MdFileParserServices;
#if DEBUG
using Kuchulem.DotNet.ConsoleHelpers.Extensions;
#endif

namespace Kuchulem.MarkdownBlog.Services
{
    /// <summary>
    /// Service providing articles from MD files
    /// </summary>
    public class ArticleService : FileModelServiceBase<Article, ArticleData>
    {
        private const string ArticlesSubdirectory = "Articles";
        private const string ArticlesFileExtension = "md";
        private const string QueryCountReadable = "count-readable";
        private readonly IFileModelCacheProvider<Article> cacheProvider;

        protected override string ModelSubDirectory => ArticlesSubdirectory;

        protected override string FileExtension => ArticlesFileExtension;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="cacheProvider"></param>
        /// <param name="fileParserService"></param>
        public ArticleService(IServicesConfiguration configuration, IFileModelCacheProvider<Article> cacheProvider, IMdFileParserService fileParserService)
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
        /// Gets the last article publicated
        /// </summary>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <param name="noCache"></param>
        /// <returns></returns>
        public IEnumerable<Article> GetLastArticles(int page, int count, bool noCache = false)
        {
#if DEBUG
            this.WriteDebugLine();
#endif
            var query = $"last:{page}:{count}";

            var articles = cacheProvider.GetQuery(query);

            if (!articles.Any() || noCache)
            {
#if DEBUG
                this.WriteDebugLine(message: "No cache");
#endif
                articles = GetReadableArticles(noCache)
                    .Skip((page - 1) * count)
                    .Take(count)
                    .ToList();

                cacheProvider.SetQuery(query, articles);
            }

#if DEBUG
            this.WriteDebugLine(message: $"return {articles.Count()} articles");
#endif
            return articles;
        }

        /// <summary>
        /// Gets the number of readable articles
        /// </summary>
        /// <param name="noCache"></param>
        /// <returns></returns>
        public int GetCountReadableArticles(bool noCache = false)
        {
            var count = cacheProvider.Request<int>(QueryCountReadable);

            if (count == default || noCache)
            {
                count = GetReadableArticles(noCache).Count();

                cacheProvider.Store(QueryCountReadable, count);
            }

            return count;
        }

        /// <summary>
        /// Gets an article from its slug
        /// </summary>
        /// <param name="slug"></param>
        /// <param name="noCache"></param>
        /// <returns></returns>
        public Article GetArticle(string slug, bool noCache = false)
        {
            return GetReadableArticles(noCache)
                .Where(a => a.Slug == slug)
                .FirstOrDefault();
        }

        /// <summary>
        /// Gets readable articles (valid articles and with publication date not after now
        /// </summary>
        /// <param name="noCache"></param>
        /// <returns></returns>
        private IEnumerable<Article> GetReadableArticles(bool noCache = false)
        {
#if DEBUG
            this.WriteDebugLine();
#endif
            var query = "readable";

            var articles = cacheProvider.GetQuery(query);

            if (!articles.Any() || noCache)
            {
#if DEBUG
                this.WriteDebugLine(message: "No cache");
#endif
                articles = GetAllArticles(noCache).Where(f => f.IsValid && f.PublicationDate <= DateTime.Now).ToList();
                cacheProvider.SetQuery(query, articles);
                cacheProvider.Store(QueryCountReadable, articles.Count());
            }

#if DEBUG
            this.WriteDebugLine(message: $"return {articles.Count()} articles");
#endif
            return articles;
        }

        /// <summary>
        /// Gets all articles
        /// </summary>
        /// <param name="noCache"></param>
        /// <returns></returns>
        private IEnumerable<Article> GetAllArticles(bool noCache = false)
        {
#if DEBUG
            this.WriteDebugLine();
#endif
            var articles = cacheProvider.All();

            if (!articles.Any() || noCache)
            {
#if DEBUG
                this.WriteDebugLine(message: "No cache");
#endif
                articles = GetAll();
                foreach (var article in articles)
                {
                    article.Author ??= configuration.DefaultAuthor;
                }

                cacheProvider.Set(articles);
            }

#if DEBUG
            this.WriteDebugLine(message: $"return {articles.Count()} articles");
#endif
            return articles;
        }
    }
}
