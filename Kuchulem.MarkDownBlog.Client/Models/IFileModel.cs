using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kuchulem.MarkDownBlog.Client.Models
{
    public interface IFileModel
    {
        /// <summary>
        /// The slug of the file (its Url friendly name) used for the resulting file name
        /// </summary>
        string Name { get; set; }
        
        /// <summary>
        /// The file extension
        /// </summary>
        string Extension { get; set; }

        /// <summary>
        /// The raw content of the file
        /// </summary>
        string RawContent { get; set; }
    }
}
