using Kuchulem.MarkdownBlog.Services.Configurations;
using Kuchulem.MarkdownBlog.Models;
using Kuchulem.MarkdownBlog.Services.CacheProvider;
using System;
using System.Collections.Generic;
using System.Linq;
#if DEBUG
using Kuchulem.MarkdownBlog.Libs.Extensions;
#endif

namespace Kuchulem.MarkdownBlog.Services
{
    public class ArticleService : FileModelServiceBase<Article>
    {
        private const string ArticlesSubdirectory = "Articles";
        private const string ArticlesFileExtension = "md";
        private const string QueryCountReadable = "count-readable";
        private readonly IFileModelCacheProvider<Article> cacheProvider;

        protected override string ModelSubDirectory => ArticlesSubdirectory;

        protected override string FileExtension => ArticlesFileExtension;

        public ArticleService(IServicesConfiguration configuration, IFileModelCacheProvider<Article> cacheProvider) 
            : base(configuration)
        {
            this.cacheProvider = cacheProvider;
        }

        public void ResetCache()
        {
            cacheProvider.ResetCache();
        }

        public IEnumerable<Article> GetLastArticles(int page, int count, bool noCache = false)
        {
#if DEBUG
            this.WriteDebugLine();
#endif
            var query = $"last:{page}:{count}";

            var articles = cacheProvider.GetQuery(query);

            if(!articles.Any() || noCache)
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

        public int GetCountReadableArticles(bool noCache = false)
        {
            var count = cacheProvider.Request<int>(QueryCountReadable);

            if(count == default || noCache)
            {
                count = GetReadableArticles(noCache).Count();

                cacheProvider.Store(QueryCountReadable, count);
            }

            return count;
        }

        public Article GetArticle(string slug, bool noCache = false)
        {
            return GetReadableArticles(noCache).Where(a => a.Slug == slug).FirstOrDefault();
        }

        private IEnumerable<Article> GetReadableArticles(bool noCache = false)
        {
#if DEBUG
            this.WriteDebugLine();
#endif
            var query = "readable";

            var articles = cacheProvider.GetQuery(query);

            if(!articles.Any() || noCache)
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

        private IEnumerable<Article> GetAllArticles(bool noCache = false)
        {
#if DEBUG
            this.WriteDebugLine();
#endif
            var articles = cacheProvider.All();

            if(!articles.Any() || noCache)
            {
#if DEBUG
                this.WriteDebugLine(message: "No cache");
#endif
                articles = GetAll();
                cacheProvider.Set(articles);
            }

#if DEBUG
            this.WriteDebugLine(message: $"return {articles.Count()} articles");
#endif
            return articles;
        }
    }
}
