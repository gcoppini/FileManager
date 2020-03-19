using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Atento.FileManager.Web.Api.Models
{
    //Dados sobre os dados - :o)
    public class FileMetadata
    {
        public string author { get; set; }
	    public string contentType { get; set; }
        public bool copyrighted { get; set; }
    }
}