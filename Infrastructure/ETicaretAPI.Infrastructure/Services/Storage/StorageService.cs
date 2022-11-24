using ETİcaretAPI.Application.Abstraction.Storage;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services.Storage
{
    public class StorageService : IStorageService
    {
        readonly IStorage storage;
        public StorageService(IStorage storage)
        {
            this.storage = storage;
        }

        public string StorageName => storage.GetType().Name;

        public async Task DeleteAsync(string pathOrContainerName, string fileName)
        {
            await storage.DeleteAsync(pathOrContainerName, fileName);
        }

        public  List<string> GetFiles(string pathOrContainerName)
        {
              return storage.GetFiles(pathOrContainerName);
        }

        public bool HasFile(string pathOrContainerName, string fileName)
        {
            return storage.HasFile(pathOrContainerName, fileName);
        }

        public Task<List<(string fileName, string pathOrContainer)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
        {
             return  storage.UploadAsync(pathOrContainerName, files);
        }
    }
}
