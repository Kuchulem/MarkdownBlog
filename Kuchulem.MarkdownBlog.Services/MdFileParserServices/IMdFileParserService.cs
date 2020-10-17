using Kuchulem.MarkdownBlog.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kuchulem.MarkdownBlog.Services.MdFileParserServices
{
    /// <summary>
    /// Interface describing the markdown files parser
    /// </summary>
    public interface IMdFileParserService
    {
        /// <summary>
        /// Parses a list of file models, converting their raw content to their html content
        /// </summary>
        /// <typeparam name="TFileData"></typeparam>
        /// <param name="fileModels"></param>
        void ParseFiles<TFileData>(IEnumerable<IFileModel> fileModels);

        /// <summary>
        /// Parses a file model, converting its raw content to its html content
        /// </summary>
        /// <typeparam name="TFileData"></typeparam>
        /// <param name="fileModel"></param>
        void ParseFile<TFileData>(IFileModel fileModel);
    }
}
