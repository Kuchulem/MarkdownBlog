using Kuchulem.MarkdownBlog.Libs.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using YamlDotNet.Serialization;

namespace Kuchulem.MarkdownBlog.Models
{
    public class Page : IFileModel
    {
        private string rawContent;

        public string Name { get; set; }

        public string Extension { get; set; }

        public string RawContent
        {
            get => rawContent;
            set
            {
                rawContent = value;
                ParseRawContent();
            }
        }

        public string Title { get; set; }

        public string HtmlContent { get; set; }

        public string Description { get; set; }

        public IEnumerable<string> Tags { get; set; } = Enumerable.Empty<string>();

        public string Slug { get; set; }

        public bool Active { get; set; }

        public bool Menu { get; set; }


        private void ParseRawContent()
        {
#if DEBUG
            this.WriteDebugLine();
#endif
            Active = false;

            using var reader = new StringReader(RawContent);
            var line = reader.ReadLine();
            if (line[0] == '#')
                Title = line.Substring(1).Trim();
            else
                return;

            do
            {
                line = reader.ReadLine();
            } while (line == "");

            if (line.StartsWith("```blog"))
                ReadPageData(reader);

            do
            {
                line = reader.ReadLine();
            } while (line == string.Empty);

            var md = reader.ReadToEnd();

            using (var writer = new StringWriter())
            {
                Markdig.Markdown.Convert(md, new Markdig.Renderers.HtmlRenderer(writer));
                HtmlContent = writer.ToString();
            }
        }

        private void ReadPageData(StringReader reader)
        {
#if DEBUG
            this.WriteDebugLine();
#endif
            var yaml = "";
            string line;
            while ((line = reader.ReadLine()) != "```")
            {
                yaml += line + Environment.NewLine;
            }

            var desrializer = new DeserializerBuilder().Build();

            var articleData = desrializer.Deserialize<PageData>(yaml);

            Tags = articleData.Tags;
            Active = articleData.Active;
            Description = articleData.Description;
            Menu = articleData.Menu;
            Slug = articleData.Slug;
        }
    }
}
