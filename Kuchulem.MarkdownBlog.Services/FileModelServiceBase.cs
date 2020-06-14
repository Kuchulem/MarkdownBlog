using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Kuchulem.MarkdownBlog.Services.Configurations;
using Kuchulem.MarkDownBlog.Models;
#if DEBUG
using Kuchulem.MarkDownBlog.Libs.Extensions;
#endif

namespace Kuchulem.MarkDownBlog.Services
{
    public abstract class FileModelServiceBase<T>
        where T : IFileModel, new()
    {
        /// <summary>
        /// Confguration of the server application
        /// </summary>
        protected readonly IServicesConfiguration configuration;

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
        public FileModelServiceBase(IServicesConfiguration configuration)
        {
#if DEBUG
            this.WriteDebugLine();
#endif
            this.configuration = configuration;

            filesPath = MakeFilesPath();
        }

        private DirectoryInfo MakeFilesPath()
        {
#if DEBUG
            this.WriteDebugLine();
#endif
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
#if DEBUG
            this.WriteDebugLine();
#endif
            return $"{name}.{FileExtension}";
        }

        /// <summary>
        /// Gets a file from its slug.
        /// </summary>
        /// <param name="fileName">The slug of the file to find</param>
        /// <returns></returns>
        protected FileInfo GetFile(string fileName)
        {
#if DEBUG
            this.WriteDebugLine();
#endif
            return new FileInfo(Path.Combine(filesPath.FullName, FileFullName(fileName)));
        }

        protected T ConvertFileToFileModel(FileInfo file)
        {
#if DEBUG
            this.WriteDebugLine();
#endif
            using var stream = file.OpenRead();
            using var reader = new StreamReader(stream);
            var content = reader.ReadToEnd();
#if DEBUG
            this.WriteDebugLine(message: "File read");
#endif
            var model = new T
            {
                Extension = FileExtension,
                Name = file.Name,
                RawContent = content
            };
#if DEBUG
            this.WriteDebugLine(message: "Model created");
#endif

            return model;
        }

        /// <summary>
        /// Gets a IFileModel instance from a file slug.<br/>
        /// Returns null if no file is found.
        /// </summary>
        /// <param name="slug">the slug of the file to find</param>
        /// <returns></returns>
        public T Get(string slug)
        {
#if DEBUG
            this.WriteDebugLine();
#endif
            var file = filesPath.GetFiles().Where(f => f.Name == slug).FirstOrDefault();

#if DEBUG
            if (!file.Exists)
                this.WriteDebugLine(message: $"File {file.FullName} not found");
#endif
            if (!file.Exists)
                return default;

            return ConvertFileToFileModel(file);
        }

        public IEnumerable<T> GetAll()
        {
#if DEBUG
            this.WriteDebugLine();
#endif
            var files = filesPath.GetFiles()
                .Where(f => f.Extension == $".{FileExtension}")
                .Select(f => ConvertFileToFileModel(f));

#if DEBUG
            this.WriteDebugLine(message: $"return {files.Count()} files");
#endif
            return files;
        }

        /// <summary>
        /// Saves a IFileModel as a file/<br/>
        /// Replace any existing file for the IFileModel slug.
        /// </summary>
        /// <param name="fileModel">The IFileModel to save</param>
        /// <returns></returns>
        public T Save(T fileModel)
        {
#if DEBUG
            this.WriteDebugLine();
#endif
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
#if DEBUG
            this.WriteDebugLine();
#endif
            return Delete(fileModel.Name);
        }

        /// <summary>
        /// Removes a file for a slug
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        public bool Delete(string slug)
        {
#if DEBUG
            this.WriteDebugLine();
#endif
            var file = GetFile(slug);

            if (file.Exists)
                file.Delete();

            return true;
        }
    }
}
