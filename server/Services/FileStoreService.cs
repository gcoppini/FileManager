using System;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using Microsoft.AspNetCore.Http;
using Atento.FileManager.Web.Api.Models;


namespace Atento.FileManager.Services
{
    /*
        CQS
        - Quais métodos são comandos e quais são querys?
        - FluentAPI
        ToDo
        - Renome repositori para MongoRepositorio
        - Revisar todas nomenclaturas, namespaces

        Docs
        - http://mongodb.github.io/mongo-csharp-driver/2.10/reference/driver/definitions/
        
    */
    public class FileStorageService : IFileStorageService
    {
        private const string FILE_AUTHOR = "Gabriel Abner Copini";
        private const bool FILE_IS_COPYRIGHTED =  true;

        private readonly IStoredFileRepository _repo;

        public FileStorageService(IStoredFileRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Save(IFormFile file)
        {
            var result = true;
            
            if (file.Length > 0)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                using (var stream = new MemoryStream())
                {
                    file.CopyTo(stream);

                    var options = new GridFSUploadOptions
                    {
                        Metadata = new BsonDocument
                        {
                            {"author", FILE_AUTHOR},
                            {"contentType", file.ContentType},
                            { "copyrighted", FILE_IS_COPYRIGHTED }
                        } 
                    };  

                    var id = await _repo.bucket.UploadFromBytesAsync(fileName, stream.ToArray(),options);
                    System.Diagnostics.Debugger.Log(1,"Info", $"New file uploaded: {id}");
                    
                }
            }
            else
            {
                result = false; 
            }

            return result;
        }

        public async Task<bool> CopyTo(string fileName,string destinationPath)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(destinationPath)) throw new ArgumentNullException();

            var result = true;

            var fileBytes = await Read(fileName);

            var fullPath = Path.Combine(@destinationPath,@fileName);

            File.WriteAllBytes(fullPath, fileBytes);
                   
            return result;

        }

        public async Task<byte[]> Read(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException();

            try
            {
                var fileBytes = await _repo.bucket.DownloadAsBytesByNameAsync(fileName);

                if(fileBytes.Length == 0) throw new FileNotFoundException();

                return fileBytes;
            }
            catch(MongoDB.Driver.GridFS.GridFSFileNotFoundException ex)
            {
                System.Diagnostics.Debugger.Log(1,"Info",ex.Message);
                throw new FileNotFoundException();
            }
        }

        public async Task<bool> Remove(string fileName)
        {
            var success = true;
            var fileId = await FindbyFilename(fileName);

            if(fileId.CompareTo(ObjectId.Empty) != 0)
            {
                await _repo.bucket.DeleteAsync(fileId);
                success = true;
            }
            else
            {
                success = false;
            }
            
            return success;

        }

        private async Task<ObjectId> FindbyFilename(string fileName)
        {

            var filter = Builders<GridFSFileInfo>.Filter.And( 
                Builders<GridFSFileInfo>.Filter.Eq(x => x.Filename, fileName)
            );

            var sort = Builders<GridFSFileInfo>.Sort.Descending(x => x.UploadDateTime);

            var options = new GridFSFindOptions
            {
                Limit = 1,
                Sort = sort
            };

            using (var cursor = await _repo.bucket.FindAsync(filter, options))
            {
                var fileInfo = (await cursor.ToListAsync()).FirstOrDefault();

                if(fileInfo!=null)
                {
                    return fileInfo.Id;
                }
            }
            
            return ObjectId.Empty;



        }

    }
}