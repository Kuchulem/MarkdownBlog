using Kuchulem.MarkDownBlog.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
#if DEBUG
using Kuchulem.MarkDownBlog.Libs.Extensions;
#endif

namespace Kuchulem.MarkDownBlog.Client.CacheProvider
{

    public class InMemoryFileCacheCacheProvider<T> : IFileModelCacheProvider<T>
        where T : IFileModel
    {
        private readonly Dictionary<string, T> Storage = new Dictionary<string, T>();

        private readonly Dictionary<string, IEnumerable<string>> QueryStorage = new Dictionary<string, IEnumerable<string>>();

        public IEnumerable<T> All()
        {
#if DEBUG
            this.WriteDebugLine();
#endif
            return Storage.Values;
        }

        public T Get(string fileName)
        {
#if DEBUG
            this.WriteDebugLine(message: fileName);
#endif
            return Storage.ContainsKey(fileName) ? Storage[fileName] : default;
        }

        public IEnumerable<T> GetQuery(string query)
        {
#if DEBUG
            this.WriteDebugLine(message: query);
#endif
            if (!QueryStorage.ContainsKey(query))
                return Enumerable.Empty<T>();

            return QueryStorage[query].Select(f => Get(f)).Where(f => f != null).ToList();
        }

        public void Set(T fileModel)
        {
#if DEBUG
            this.WriteDebugLine(message: fileModel.Name);
#endif
            Storage[fileModel.Name] = fileModel;
        }

        public void Set(IEnumerable<T> fileModels)
        {
#if DEBUG
            this.WriteDebugLine($"{fileModels.Count()} models");
#endif
            foreach (var fileModel in fileModels)
                Set(fileModel);
        }

        public void SetQuery(string query, IEnumerable<T> fileModels)
        {
#if DEBUG
            this.WriteDebugLine(message: query);
#endif
            QueryStorage[query] = fileModels.Select(f => f.Name).ToList();
        }
    }
}
