using Kuchulem.MarkDownBlog.Client.Configuration;
using Kuchulem.MarkDownBlog.Client.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Kuchulem.MarkDownBlog.Client.Services
{
    public abstract class FileModelServiceBase<T>
        where T : IFileModel, new()
    {
        /// <summary>
        /// Confguration of the server application
        /// </summary>
        protected readonly ApplicationConfiguration configuration;

        /// <summary>
        /// The directory where files are stored for that 
        /// </summary>
        protected readonly DirectoryInfo filesPath;

        /// <summary>
        /// The sub directory where the model files are stored
        /// </summary>
        protected abstract string ModelSubDirectory { get; }

        /// <summary>
        /// The extension expected for those files
        /// </summary>
        protected abstract string FileExtension { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">The configuration of the application</param>
        public FileModelServiceBase(ApplicationConfiguration configuration)
        {
            this.configuration = configuration;

            filesPath = MakeFilesPath();
        }

        private DirectoryInfo MakeFilesPath()
        {
            var dirPath = Path.Combine(configuration.DataFilesPath, ModelSubDirectory);

            if (!Directory.Exists(dirPath))
            {
                try
                {
                    return Directory.CreateDirectory(dirPath);
                }
                catch(UnauthorizedAccessException e)
                {
                    throw new Exception($"You have not the permission to create {dirPath}.", e);
                }
                catch(NotSupportedException e)
                {
                    throw new Exception($"{dirPath} contains a forbidden caracter.", e);
                }
                catch(IOException e)
                {
                    throw new Exception($"{dirPath} is not a valid path.", e);
                }
            }
            else
                return new DirectoryInfo(dirPath);
        }

        /// <summary>
        /// Makes a file name from the slug of the file and its extension
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected string FileFullName(string name)
        {
            return $"{name}.{FileExtension}";
        }

        /// <summary>
        /// Gets a file from its slug.
        /// </summary>
        /// <param name="slug">The slug of the file to find</param>
        /// <returns></returns>
        protected FileInfo GetFile(string slug)
        {
            return new FileInfo(Path.Combine(filesPath.FullName, FileFullName(slug)));
        }

        protected T ConvertFileToFileModel(FileInfo file)
        {
            using var stream = file.OpenRead();
            using var reader = new StreamReader(stream);
            return new T
            {
                Extension = FileExtension,
                Name = file.Name,
                RawContent = reader.ReadToEnd()
            };
        }

        /// <summary>
        /// Gets a IFileModel instance from a file slug.<br/>
        /// Returns null if no file is found.
        /// </summary>
        /// <param name="slug">the slug of the file to find</param>
        /// <returns></returns>
        public T Get(string slug)
        {
            var file = filesPath.GetFiles().Where(f => f.Name == slug).FirstOrDefault();

            if (!file.Exists)
                return default;

            return ConvertFileToFileModel(file);
        }

        public IEnumerable<T> GetAll()
        {
            var files = filesPath.GetFiles();
            return files
                .Where(f => f.Extension == $".{FileExtension}")
                .Select(f => ConvertFileToFileModel(f));
        }

        /// <summary>
        /// Saves a IFileModel as a file/<br/>
        /// Replace any existing file for the IFileModel slug.
        /// </summary>
        /// <param name="fileModel">The IFileModel to save</param>
        /// <returns></returns>
        public T Save(T fileModel)
        {
            var file = GetFile(fileModel.Name);

            using var stream = file.OpenWrite();
            using var writer = new StreamWriter(stream);
            writer.Write(fileModel.RawContent);

            return Get(fileModel.Name);
        }

        /// <summary>
        /// Removes a file for a IFileModel
        /// </summary>
        /// <param name="fileModel"></param>
        /// <returns></returns>
        public bool Delete(T fileModel)
        {
            return Delete(fileModel.Name);
        }

        /// <summary>
        /// Removes a file for a slug
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        public bool Delete(string slug)
        {
            var file = GetFile(slug);

            if (file.Exists)
                file.Delete();

            return true;
        }
    }
}
