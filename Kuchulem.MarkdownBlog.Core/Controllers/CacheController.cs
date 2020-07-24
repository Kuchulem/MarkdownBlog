using Kuchulem.MarkdownBlog.Client.Extensions;
using Kuchulem.MarkdownBlog.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Kuchulem.MarkdownBlog.Core.Controllers
{
    public class CacheController : Controller
    {
        private readonly ArticleService articleService;
        private readonly PageService pageService;

        public CacheController(ArticleService articleService, PageService pageService)
        {
            this.articleService = articleService;
            this.pageService = pageService;
        }

        public IActionResult Reload(string key, string scope)
        {
            var scopes = scope switch
            {
                "Articles" => new[] { "Articles" },
                "Pages" => new[] { "Pages" },
                "All" => new[] { "Articles", "Pages" },
                _ => new string[] { }
            };

            if (!ValidateKey(key))
                return NotFound();

            if (!scopes.Any())
                return BadRequest(scope);

            foreach(var foundScope in scopes)
                switch (foundScope)
                {
                    case "Articles":
                        articleService.ResetCache();
                        break;
                    case "Pages":
                        pageService.ResetCache();
                        break;
                    default:
                        throw new Exception("Invalid scope");
                }

            return Ok();
        }

        private bool ValidateKey(string key)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
#if DEBUG
            this.WriteDebugLine(message: $"validating key {key}");
            this.WriteDebugLine(message: $"Searching key file in {currentDirectory}");
#endif
            var path = Path.Combine(currentDirectory, "command_access_key.key");

            if (!System.IO.File.Exists(path))
            {
#if DEBUG
                this.WriteDebugLine(message: $"Access key file not found : {path}");
#endif
                return false;
            }

            using var stream = System.IO.File.OpenRead(path);
            using var reader = new StreamReader(stream);
            var concurrentKey = reader.ReadToEnd().Trim();

#if DEBUG
            this.WriteDebugLine(message: $"Found concurrent key : {concurrentKey}");
#endif

            return key == concurrentKey;
        }
    }
}
