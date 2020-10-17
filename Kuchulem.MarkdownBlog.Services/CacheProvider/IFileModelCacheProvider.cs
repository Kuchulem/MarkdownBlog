using Kuchulem.MarkdownBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kuchulem.MarkdownBlog.Services.CacheProvider
{
    /// <summary>
    /// Interface describing the cache provider for file models
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFileModelCacheProvider<T>
        where T : IFileModel
    {
        /// <summary>
        /// Gets all files from the cache
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> All();

        /// <summary>
        /// Gets a file from cache
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        T Get(string fileName);

        /// <summary>
        /// Sets a file to the cache
        /// </summary>
        /// <param name="fileModel"></param>
        void Set(T fileModel);

        /// <summary>
        /// Sets a list of files in the cache
        /// </summary>
        /// <param name="fileModels"></param>
        void Set(IEnumerable<T> fileModels);

        /// <summary>
        /// Gets the cache for a query
        /// </summary>
        /// <param name="queryName"></param>
        /// <returns></returns>
        IEnumerable<T> GetQuery(string query);

        /// <summary>
        /// associates a list of file models to a query
        /// </summary>
        /// <param name="queryName"></param>
        /// <param name="fileModels"></param>
        void SetQuery(string query, IEnumerable<T> fileModels);

        /// <summary>
        /// Stores data to a query
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="queryName"></param>
        /// <param name="data"></param>
        void Store<TData>(string query, TData data);

        /// <summary>
        /// Requests data for a query
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="queryName"></param>
        /// <returns></returns>
        TData Request<TData>(string query);

        /// <summary>
        /// Clears the cache
        /// </summary>
        void ClearCache();
    }
}
