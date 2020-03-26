namespace Atento.FileManager.Web.Api.Models
{
    using MongoDB.Driver;
     using MongoDB.Driver.GridFS;

    public interface IStoredFileContext
    {
        GridFSBucket bucket { get; }
        IMongoCollection<StoredFile> Files { get; }
    }
}