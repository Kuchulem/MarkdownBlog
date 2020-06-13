using Kuchulem.MarkDownBlog.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kuchulem.MarkDownBlog.Client.CacheProvider
{

    public class InMemoryFileCacheCacheProvider<T> : IFileModelCacheProvider<T>
        where T : IFileModel
    {
        private readonly Dictionary<string, T> Storage = new Dictionary<string, T>();

        private readonly Dictionary<string, IEnumerable<string>> QueryStorage = new Dictionary<string, IEnumerable<string>>();

        public IEnumerable<T> All()
        {
            return Storage.Values;
        }

        public T Get(string fileName)
        {
            return Storage.ContainsKey(fileName) ? Storage[fileName] : default;
        }

        public IEnumerable<T> GetQuery(string query)
        {
            if (!QueryStorage.ContainsKey(query))
                return Enumerable.Empty<T>();

            return QueryStorage[query].Select(f => Get(f)).Where(f => f != null);
        }

        public void Set(T fileModel)
        {
            Storage[fileModel.Name] = fileModel;
        }
        public void Set(IEnumerable<T> fileModels)
        {
            foreach (var fileModel in fileModels)
                Set(fileModel);
        }


        public void SetQuery(string query, IEnumerable<T> fileModels)
        {
            QueryStorage[query] = fileModels.Select(f => f.Name);
        }
    }
}
