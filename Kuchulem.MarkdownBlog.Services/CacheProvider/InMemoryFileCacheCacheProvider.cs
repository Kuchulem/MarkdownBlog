using Kuchulem.MarkdownBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
#if DEBUG
using Kuchulem.DotNet.ConsoleHelpers.Extensions;
#endif

namespace Kuchulem.MarkdownBlog.Services.CacheProvider
{
    /// <summary>
    /// Cache provider storing in the application memory. Will be destroyed when the application is shut down
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class InMemoryFileCacheCacheProvider<T> : IFileModelCacheProvider<T>
        where T : IFileModel
    {
        private readonly Dictionary<string, object> alternatStorage = new Dictionary<string, object>();

        private readonly Dictionary<string, T> storage = new Dictionary<string, T>();

        private readonly Dictionary<string, IEnumerable<string>> queryStorage = new Dictionary<string, IEnumerable<string>>();

        /// <summary>
        /// see <see cref="IFileModelCacheProvider{T}.All"/>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> All()
        {
#if DEBUG
            this.WriteDebugLine();
#endif
            return storage.Values;
        }

        /// <summary>
        /// see <see cref="IFileModelCacheProvider{T}.Get(string)"/>
        /// </summary>
        /// <returns></returns>
        public T Get(string fileName)
        {
#if DEBUG
            this.WriteDebugLine(message: fileName);
#endif
            return storage.ContainsKey(fileName) ? storage[fileName] : default;
        }

        /// <summary>
        /// see <see cref="IFileModelCacheProvider{T}.GetQuery(string)"/>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetQuery(string queryName)
        {
#if DEBUG
            this.WriteDebugLine(message: queryName);
#endif
            if (!queryStorage.ContainsKey(queryName))
                return Enumerable.Empty<T>();

            return queryStorage[queryName].Select(f => Get(f)).Where(f => f != null).ToList();
        }

        /// <summary>
        /// see <see cref="IFileModelCacheProvider{T}.Set(T)"/>
        /// </summary>
        /// <returns></returns>
        public void Set(T fileModel)
        {
#if DEBUG
            this.WriteDebugLine(message: fileModel.Name);
#endif
            storage[fileModel.Name] = fileModel;
        }

        /// <summary>
        /// see <see cref="IFileModelCacheProvider{T}.Set(IEnumerable{T})"/>
        /// </summary>
        /// <returns></returns>
        public void Set(IEnumerable<T> fileModels)
        {
#if DEBUG
            this.WriteDebugLine($"{fileModels.Count()} models");
#endif
            foreach (var fileModel in fileModels)
                Set(fileModel);
        }

        /// <summary>
        /// see <see cref="IFileModelCacheProvider{T}.SetQuery(string, IEnumerable{T})"/>
        /// </summary>
        /// <returns></returns>
        public void SetQuery(string queryName, IEnumerable<T> fileModels)
        {
#if DEBUG
            this.WriteDebugLine(message: queryName);
#endif
            queryStorage[queryName] = fileModels.Select(f => f.Name).ToList();
        }

        /// <summary>
        /// see <see cref="IFileModelCacheProvider{T}.Store{TData}(string, TData)"/>
        /// </summary>
        /// <returns></returns>
        public void Store<TData>(string queryName, TData data)
        {
#if DEBUG
            this.WriteDebugLine(message: queryName);
#endif
            alternatStorage[queryName] = data;
        }

        /// <summary>
        /// see <see cref="IFileModelCacheProvider{T}.Request{TData}(string)"/>
        /// </summary>
        /// <returns></returns>
        public TData Request<TData>(string queryName)
        {
#if DEBUG
            this.WriteDebugLine(message: queryName);
#endif
            if (!alternatStorage.ContainsKey(queryName))
                return default;

            return (TData)alternatStorage[queryName];
        }

        /// <summary>
        /// see <see cref="IFileModelCacheProvider{T}.ClearCache"/>
        /// </summary>
        /// <returns></returns>
        public void ClearCache()
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
