namespace Atento.FileManager.Web.Api.Models
{
    using Atento.FileManager.Web.Api;
    using MongoDB.Driver;
    using MongoDB.Driver.GridFS;
    
    public class StoredFileContext: IStoredFileContext
    {
        private readonly IMongoDatabase _db;
        public GridFSBucket bucket { get; private set;}

        public StoredFileContext(MongoDBConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            _db = client.GetDatabase(config.Database);
            bucket = new GridFSBucket(_db);
        }

        public IMongoCollection<StoredFile> Files => _db.GetCollection<StoredFile>("fs.files");

    }
}