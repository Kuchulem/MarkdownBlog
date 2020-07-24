using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YamlDotNet.Core;
using YamlDotNet.Serialization;
#if DEBUG
using Kuchulem.MarkdownBlog.Libs.Extensions;
#endif

namespace Kuchulem.MarkdownBlog.Models
{
    public class Article : IFileModel
    {
        private string rawContent;

        /// <see cref="IFileModel.Name"/>
        public string Name { get; set; }

        /// <see cref="IFileModel.Extension"/>
        public string Extension { get; set; }

        public string Slug { get; set; }

        /// <see cref="IFileModel.RawContent"/>
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

        public IEnumerable<string> Tags { get; set; }

        public string MainPicture { get; set; }

        public string Thumbnail { get; set; }

        public DateTime PublicationDate { get; set; }

        public string Summary { get; set; }

        public bool IsValid { get; set; }

        /// <summary>
        /// Html content of the article
        /// </summary>
        public string HtmlContent { get; private set; }

        private void ParseRawContent()
        {
#if DEBUG
            this.WriteDebugLine();
#endif
            IsValid = false;

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
                ReadArticleData(reader);

            do
            {
                line = reader.ReadLine();
            } while (line == "");

            Summary = "";

            while (!string.IsNullOrEmpty(line))
            {
                Summary += line + "\n";
                line = reader.ReadLine();
            }

            using (var writer = new StringWriter())
            {
                Markdig.Markdown.Convert(Summary, new Markdig.Renderers.HtmlRenderer(writer));
                Summary = writer.ToString();
            }

            var md = reader.ReadToEnd();

            using (var writer = new StringWriter())
            {
                Markdig.Markdown.Convert(md, new Markdig.Renderers.HtmlRenderer(writer));
                HtmlContent = writer.ToString();
            }

            IsValid = true;
        }

        private void ReadArticleData(StringReader reader)
        {
#if DEBUG
            this.WriteDebugLine();
#endif
            var yaml = "";
            string line;
            while((line = reader.ReadLine()) != "```")
            {
                yaml += line + Environment.NewLine;
            }

            var desrializer = new DeserializerBuilder().Build();

            var articleData = desrializer.Deserialize<ArticleData>(yaml);

            Tags = articleData.Tags;
            MainPicture = articleData.Picture.Main;
            Thumbnail = articleData.Picture.Thumbnail;
            PublicationDate = articleData.PublicationDate;
            Slug = articleData.Slug;
        }
    }
}
