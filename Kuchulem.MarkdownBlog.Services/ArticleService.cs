using Kuchulem.MarkdownBlog.Services.Configurations;
using Kuchulem.MarkDownBlog.Models;
using Kuchulem.MarkDownBlog.Services.CacheProvider;
using System;
using System.Collections.Generic;
using System.Linq;
#if DEBUG
using Kuchulem.MarkDownBlog.Libs.Extensions;
#endif

namespace Kuchulem.MarkDownBlog.Services
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

        public IEnumerable<Article> GetLastArticles(int page, int count)
        {
#if DEBUG
            this.WriteDebugLine();
#endif
            var query = $"last:{page}:{count}";

            var articles = cacheProvider.GetQuery(query);

            if(!articles.Any())
            {
#if DEBUG
                this.WriteDebugLine(message: "No cache");
#endif
                articles = GetReadableArticles()
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

        public int GetCountReadableArticles()
        {
            var count = cacheProvider.Request<int>(QueryCountReadable);

            if(count == default)
            {
                count = GetReadableArticles().Count();

                cacheProvider.Store(QueryCountReadable, count);
            }

            return count;
        }

        public Article GetArticle(string slug)
        {
            return GetReadableArticles().Where(a => a.Slug == slug).FirstOrDefault();
        }

        private IEnumerable<Article> GetReadableArticles()
        {
#if DEBUG
            this.WriteDebugLine();
#endif
            var query = "readable";

            var articles = cacheProvider.GetQuery(query);

            if(!articles.Any())
            {
#if DEBUG
                this.WriteDebugLine(message: "No cache");
#endif
                articles = GetAllArticles().Where(f => f.IsValid && f.PublicationDate <= DateTime.Now).ToList();
                cacheProvider.SetQuery(query, articles);
                cacheProvider.Store(QueryCountReadable, articles.Count());
            }

#if DEBUG
            this.WriteDebugLine(message: $"return {articles.Count()} articles");
#endif
            return articles;
        }

        private IEnumerable<Article> GetAllArticles()
        {
#if DEBUG
            this.WriteDebugLine();
#endif
            var articles = cacheProvider.All();

            if(!articles.Any())
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
