using Kuchulem.MarkDownBlog.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kuchulem.MarkDownBlog.Client.CacheProvider
{
    public interface IFileModelCacheProvider<T>
        where T : IFileModel
    {
        IEnumerable<T> All();

        T Get(string fileName);

        void Set(T fileModel);

        void Set(IEnumerable<T> fileModels);

        IEnumerable<T> GetQuery(string query);

        void SetQuery(string query, IEnumerable<T> fileModels);
    }
}
