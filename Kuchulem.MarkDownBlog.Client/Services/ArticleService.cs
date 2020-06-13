using Kuchulem.MarkDownBlog.Client.CacheProvider;
using Kuchulem.MarkDownBlog.Client.Configuration;
using Kuchulem.MarkDownBlog.Client.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var query = $"last:{page}:{count}";

            var articles = cacheProvider.GetQuery(query);

            if(!articles.Any())
            {
                articles = GetReadableArticles()
                    .Skip((page - 1) * count)
                    .Take(count);
                cacheProvider.SetQuery(query, articles);
            }

            return articles;
        }

        private IEnumerable<Article> GetReadableArticles()
        {
            var query = "readable";

            var articles = cacheProvider.GetQuery(query);

            if(!articles.Any())
            {
                articles = GetAllArticles().Where(f => f.IsValid && f.PublicationDate <= DateTime.Now);
                cacheProvider.SetQuery(query, articles);
            }

            return articles;
        }

        private IEnumerable<Article> GetAllArticles()
        {
            var articles = cacheProvider.All();

            if(!articles.Any())
            {
                articles = GetAll();
                cacheProvider.Set(articles);
            }

            return articles;
        }
    }
}
