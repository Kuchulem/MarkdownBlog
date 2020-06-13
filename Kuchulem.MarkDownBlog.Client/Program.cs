using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kuchulem.MarkDownBlog.Client.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Kuchulem.MarkDownBlog.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var test = new ArticleData
            //{
            //    Picture = new ArticlePicture { Main = "pic-main.jpg", Thumbnail = "pin-thumb.jpg" },
            //    PublicationDate = DateTime.Now,
            //    Tags = new[] { "tag1", "tag2", "tag3" }
            //};


            //var yml = new YamlDotNet.Serialization.Serializer().Serialize(test);


            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
