using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kuchulem.MarkdownBlog.Core.Models
{
    public interface IPageViewModel
    {
        string Title { get; }
        string Summary { get; }
        string Author { get; }
    }
}
