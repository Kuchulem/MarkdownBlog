using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kuchulem.MarkDownBlog.Models
{
    public class ArticleData
    {
        public string Slug { get; set; }

        public IEnumerable<string> Tags { get; set; }

        public ArticlePicture Picture { get; set; }

        public DateTime PublicationDate { get; set; }
    }
}
