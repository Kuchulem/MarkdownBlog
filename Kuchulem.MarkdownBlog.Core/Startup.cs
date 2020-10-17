using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kuchulem.MarkdownBlog.Services.Configurations;
using Kuchulem.MarkdownBlog.Core.Configuration;
using Kuchulem.MarkdownBlog.Models;
using Kuchulem.MarkdownBlog.Services;
using Kuchulem.MarkdownBlog.Services.CacheProvider;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Kuchulem.MarkdownBlog.Services.MdFileParserServices;
using Markdig;
using Kuchulem.MarkdownBlog.Services.MarkdownExtensions;
using Kuchulem.MarkdownBlog.Services.MarkdownExtensions.LastArticles;
using Microsoft.AspNetCore.Mvc.Razor;
using Kuchulem.MarkdownBlog.Services.ViewRendererService;
using Kuchulem.MarkdownBlog.Core.Models.Articles;

namespace Kuchulem.MarkdownBlog.Core
{
    public class Startup
    {
        private readonly ApplicationConfiguration appConfiguration;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            appConfiguration = Configuration.GetSection("Application").Get<ApplicationConfiguration>();

            if (appConfiguration == null || !appConfiguration.IsComplete)
                throw new Exception("Configuration is incomplete.");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IServicesConfiguration>(appConfiguration);

            services.AddSingleton<IFileModelCacheProvider<Article>>(new InMemoryFileCacheCacheProvider<Article>());
            services.AddSingleton<IFileModelCacheProvider<Page>>(new InMemoryFileCacheCacheProvider<Page>());

            services.AddTransient<ViewRendererService>();
            services.AddTransient((services) =>
            {
                return new LastArticlesOptions(
                    () => services.GetService<ArticleService>(),
                    (article) => services.GetService<ViewRendererService>().RenderToStringAsync("Articles/_ArticleSummary", new ArticleSummaryViewModel
                    {
                        Author = article.Author,
                        MainPicture = article.Picture.Main,
                        PublicationDate = article.PublicationDate,
                        Slug = article.Slug,
                        Summary = article.Summary,
                        Tags = article.Tags,
                        Title = article.Title
                    }).GetAwaiter().GetResult()
                )
                {
                    ArticleUrlFormat = "/Articles/{0}",
                    DefaultAuthor = appConfiguration.DefaultAuthor
                };
            });
            services.AddTransient((services) =>
            {
                return new MarkdownPipelineBuilder()
                    .UseLastArticles(services.GetService<LastArticlesOptions>())
                    .UseLastArticle(services.GetService<LastArticlesOptions>())
                    .Build();
            });
            services.AddTransient<IMdFileParserService, MarkDigParserService>();
            services.AddTransient<ArticleService>();
            services.AddTransient<PageService>();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
