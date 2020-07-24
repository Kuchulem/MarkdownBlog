using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
#if DEBUG
using Kuchulem.MarkdownBlog.Libs.Extensions;
using Kuchulem.MarkdownBlog.Models;
#endif

namespace Kuchulem.MarkdownBlog.Services.CacheProvider
{

    public class InMemoryFileCacheCacheProvider<T> : IFileModelCacheProvider<T>
        where T : IFileModel
    {
        private readonly Dictionary<string, object> alternatStorage = new Dictionary<string, object>();

        private readonly Dictionary<string, T> storage = new Dictionary<string, T>();

        private readonly Dictionary<string, IEnumerable<string>> queryStorage = new Dictionary<string, IEnumerable<string>>();

        public IEnumerable<T> All()
        {
#if DEBUG
            this.WriteDebugLine();
#endif
            return storage.Values;
        }

        public T Get(string fileName)
        {
#if DEBUG
            this.WriteDebugLine(message: fileName);
#endif
            return storage.ContainsKey(fileName) ? storage[fileName] : default;
        }

        public IEnumerable<T> GetQuery(string query)
        {
#if DEBUG
            this.WriteDebugLine(message: query);
#endif
            if (!queryStorage.ContainsKey(query))
                return Enumerable.Empty<T>();

            return queryStorage[query].Select(f => Get(f)).Where(f => f != null).ToList();
        }

        public void Set(T fileModel)
        {
#if DEBUG
            this.WriteDebugLine(message: fileModel.Name);
#endif
            storage[fileModel.Name] = fileModel;
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
            queryStorage[query] = fileModels.Select(f => f.Name).ToList();
        }

        public void Store<TData>(string query, TData data)
        {
#if DEBUG
            this.WriteDebugLine(message: query);
#endif
            alternatStorage[query] = data;
        }

        public TData Request<TData>(string query)
        {
#if DEBUG
            this.WriteDebugLine(message: query);
#endif
            if (!alternatStorage.ContainsKey(query))
                return default;

            return (TData)alternatStorage[query];
        }

        public void ResetCache()
        {
#if DEBUG
            this.WriteDebugLine(message: "reset cache");
#endif
            alternatStorage.Clear();
            storage.Clear();
            queryStorage.Clear();
        }
    }
}
