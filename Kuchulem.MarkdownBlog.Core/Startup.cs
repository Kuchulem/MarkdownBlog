using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kuchulem.MarkdownBlog.Services.Configurations;
using Kuchulem.MarkDownBlog.Core.Configuration;
using Kuchulem.MarkDownBlog.Models;
using Kuchulem.MarkDownBlog.Services;
using Kuchulem.MarkDownBlog.Services.CacheProvider;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

            services.AddTransient<ArticleService>();

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
