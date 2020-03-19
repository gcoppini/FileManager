namespace Atento.FileManager.Web.Api.Models
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
     using MongoDB.Driver.GridFS;

    public interface IStoredFileRepository
    {
        GridFSBucket bucket { get; set;}

        // api/[GET]
        Task<IEnumerable<StoredFile>> GetAllFiles();

        // api/1/[GET]
        Task<StoredFile> GetFile(string id);

        // api/[POST]
        Task Create(StoredFile file);

        // api/[PUT]
        Task<bool> Update(StoredFile file);

        // api/1/[DELETE]
        Task<bool> Delete(string id);

        Task<long> GetNextId();
    }
}