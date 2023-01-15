using System;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ETİcaretAPI.Application.Abstraction.Storage.Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ETicaretAPI.Infrastructure.Services.Storage.Azure
{
    public class AzureStorage :Storage, IAzureStorage
    {
        //İlgili azure strorage accountuna bağlanmamıza sağlar.
        readonly BlobServiceClient _blobserviceClient;

        //Bağlanılan hesaptaki hedef container üzerinde dosya işlemleri yapmamızı sağlar.
        BlobContainerClient _blobContainerClient;

        public AzureStorage(IConfiguration configuration)
        {
            _blobserviceClient = new BlobServiceClient(configuration["Storage:Azure"]);
        }

        public async Task DeleteAsync(string containerName, string fileName)
        {
            _blobContainerClient = _blobserviceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);
            await blobClient.DeleteAsync();
        }

        public List<string> GetFiles(string containerName)
        {
            _blobContainerClient = _blobserviceClient.GetBlobContainerClient(containerName);
            return _blobContainerClient.GetBlobs().Select(x=>x.Name).ToList();
        }

        public bool HasFile(string containerName, string fileName)
        {
            _blobContainerClient = _blobserviceClient.GetBlobContainerClient(containerName);
            return _blobContainerClient.GetBlobs().Any(x => x.Name ==fileName);
        }

        public async Task<List<(string fileName, string pathOrContainer)>> UploadAsync(string containerName, IFormFileCollection files)
        {
            _blobContainerClient = _blobserviceClient.GetBlobContainerClient(containerName);
            await _blobContainerClient.CreateIfNotExistsAsync();
            await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

            List<(string fileName, string pathOrContainer)> datas = new();

            foreach (var file in files)
            {
             string newFileName=  await FileRenameAsync(containerName,HasFile ,file.Name);

              BlobClient blobClient=  _blobContainerClient.GetBlobClient(newFileName);
               await blobClient.UploadAsync(file.OpenReadStream());
                datas.Add((file.Name, $"{containerName}/{newFileName}"));
                
            }

            return datas;
        }
    }
}

