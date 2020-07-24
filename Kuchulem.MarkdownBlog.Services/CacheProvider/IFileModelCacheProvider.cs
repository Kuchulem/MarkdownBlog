using Kuchulem.MarkdownBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kuchulem.MarkdownBlog.Services.CacheProvider
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

        void Store<TData>(string query, TData data);

        TData Request<TData>(string query);

        void ResetCache();
    }
}
