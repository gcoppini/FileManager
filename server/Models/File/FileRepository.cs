namespace Atento.FileManager.Web.Api.Models
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MongoDB.Driver;
    using MongoDB.Bson;
    using System.Linq;
    using MongoDB.Driver.GridFS;


    public class StoredFileRepository : IStoredFileRepository
    {
        internal readonly IStoredFileContext _context;
        public GridFSBucket bucket {get;set;}

        public StoredFileRepository(IStoredFileContext context)
        {
            _context = context;
            bucket = context.bucket;
        }

        public async Task<IEnumerable<StoredFile>> GetAllFiles()
        {
            return await _context
                            .Files
                            .Find(_ => true)
                            .ToListAsync();
        }
        public Task<StoredFile> GetFile(string id)
        {
            FilterDefinition<StoredFile> filter = Builders<StoredFile>.Filter.Eq(m => m.Id, id);
            return _context
                    .Files
                    .Find(filter)
                    .FirstOrDefaultAsync();
        }
        public async Task Create(StoredFile file)
        {
            await _context.Files.InsertOneAsync(file);
        }
        public async Task<bool> Update(StoredFile file)
        {
            ReplaceOneResult updateResult =
                await _context
                        .Files
                        .ReplaceOneAsync(
                            filter: g => g.Id == file.Id,
                            replacement: file);
            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
        public async Task<bool> Delete(string id)
        {
            FilterDefinition<StoredFile> filter = Builders<StoredFile>.Filter.Eq(m => m.Id, id);
            DeleteResult deleteResult = await _context
                                                .Files
                                                .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
        public async Task<long> GetNextId()
        {
            return await _context.Files.CountDocumentsAsync(new BsonDocument()) + 1;
        }
    }
}