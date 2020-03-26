
using System.Collections.Generic;
using System.Threading.Tasks;
using Atento.FileManager.Common.Data.Maybe;
using Microsoft.AspNetCore.Http;

namespace Atento.FileManager.Services
{
    public interface IFileStorageService
    {
        Task<bool> Save(IFormFile file);
        Task<bool> CopyTo(string fileName, string destinationPath);
        Task<byte[]> Read(string fileName);
        Task<bool> Remove(string fileName);
    }
}