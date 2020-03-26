using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Atento.FileManager.Web.Api.Models
{
    public class StoredFile
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        public string Id { get; set; }
        
        public long length { get; set; }
        public long chunkSize { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime uploadDate { get; set; }
        public string md5 { get; set; }
	    public string filename { get; set; }
        public FileMetadata metadata { get; set; } 
    }
}