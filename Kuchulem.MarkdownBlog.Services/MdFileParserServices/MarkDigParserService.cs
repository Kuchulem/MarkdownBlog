using Kuchulem.MarkdownBlog.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using YamlDotNet.Serialization;
using System.Text.RegularExpressions;
using Markdig;
using Markdig.Extensions.Tables;
#if DEBUG
using Kuchulem.DotNet.ConsoleHelpers.Extensions;
#endif

namespace Kuchulem.MarkdownBlog.Services.MdFileParserServices
{
    /// <summary>
    /// Parser based on Mardig markdown parser
    /// </summary>
    public class MarkDigParserService : IMdFileParserService
    {
        private readonly MarkdownPipeline pipeline;

        public MarkDigParserService(MarkdownPipeline pipeline)
        {
            this.pipeline = pipeline;
        }

        /// <summary>
        /// see <see cref="IMdFileParserService.ParseFile{TFileData}(IFileModel)"/>
        /// </summary>
        /// <typeparam name="TFileData"></typeparam>
        /// <param name="fileModel"></param>
        public void ParseFile<TFileData>(IFileModel fileModel)
        {
#if DEBUG
            this.WriteDebugLine();
#endif
            fileModel.IsValid = false;

            using var reader = new StringReader(fileModel.RawContent);
            var line = reader.ReadLine();
            if (line[0] == '#')
                fileModel.Title = line.Substring(1).Trim();
            else
                return;

            do
            {
                line = reader.ReadLine();
            } while (line == "");

            if (line.StartsWith("[[DocumentData"))
                ReadData<TFileData>(reader, fileModel);

            do
            {
                line = reader.ReadLine();
            } while (line == "");


            if (line.StartsWith("[[Summary"))
                ReadSummary(reader, fileModel);

            var markdown = reader.ReadToEnd();

            using var writer = new StringWriter();
            fileModel.HtmlContent = Markdown.ToHtml(markdown.Trim(), pipeline);

            fileModel.IsValid = true;
        }

        /// <summary>
        /// see <see cref="IMdFileParserService.ParseFiles{TFileData}(IEnumerable{IFileModel})"/>
        /// </summary>
        /// <typeparam name="TFileData"></typeparam>
        /// <param name="fileModels"></param>
        public void ParseFiles<TFileData>(IEnumerable<IFileModel> fileModels)
        {
            fileModels.AsParallel().ForAll(m => ParseFile<TFileData>(m));
        }

        private void ReadData<TFileData>(StringReader reader, IFileModel fileModel)
        {
#if DEBUG
            this.WriteDebugLine();
#endif
            var yaml = "";
            string line;
            while (!(line = reader.ReadLine()).EndsWith("]]"))
            {
                yaml += line + Environment.NewLine;
            }

            var desrializer = new DeserializerBuilder().Build();

            var fileData = desrializer.Deserialize<TFileData>(yaml);

            foreach (var prop in typeof(TFileData).GetProperties())
            {
                var fileProp = fileModel.GetType().GetProperty(prop.Name);

                if (fileProp is null)
                {
#if DEBUG
                    this.WriteDebugLine(message: string.Format(
                        "Missing property {0} from data <{1}> in model <{2}>",
                        prop.Name,
                        typeof(TFileData).Name,
                        fileModel.GetType().Name
                    ));
#endif
                    continue;
                }

                var value = prop.GetValue(fileData);

                if (value is string stringValue)
                    value = stringValue.Trim();

                fileProp.SetValue(fileModel, value);
            }
        }

        private void ReadSummary(StringReader reader, IFileModel fileModel)
        {
#if DEBUG
            this.WriteDebugLine();
#endif
            var summary = "";
            string line;
            while (!(line = reader.ReadLine()).EndsWith("]]"))
            {
                summary += line.Trim() + Environment.NewLine;
            }

            fileModel.Summary = summary.Trim();
        }
    }
}
