using Kuchulem.MarkDownBlog.Client.CacheProvider;
using Kuchulem.MarkDownBlog.Client.Configuration;
using Kuchulem.MarkDownBlog.Client.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
#if DEBUG
using Kuchulem.MarkDownBlog.Libs.Extensions;
#endif

namespace Kuchulem.MarkDownBlog.Client.Services
{
    public class ArticleService : FileModelServiceBase<Article>
    {
        private const string SUBDIRECTORY = "Articles";
        private const string FILE_EXTENSION = "md";
        private readonly IFileModelCacheProvider<Article> cacheProvider;

        protected override string ModelSubDirectory => SUBDIRECTORY;

        protected override string FileExtension => FILE_EXTENSION;

        public ArticleService(ApplicationConfiguration configuration, IFileModelCacheProvider<Article> cacheProvider) : base(configuration)
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

        public IEnumerable<Article> GetArticle(string slug)
        {
            return GetReadableArticles().Where(a => a.Slug == slug).ToList();
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
