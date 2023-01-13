using ETİcaretAPI.Application.Abstraction.Storage.Local;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services.Storage.Local
{
    public class LocalStorage : Storage,ILocalStorage
    {
        readonly IWebHostEnvironment webHostEnvironment;

        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task DeleteAsync(string path, string fileName)
        {
             File.Delete($"{path}\\{fileName}");
        }

        public List<string> GetFiles(string path)
        {
            DirectoryInfo directory = new(path);
            return directory.GetFiles().Select(x=> x.Name).ToList();
        }

        public bool HasFile(string path, string fileName)
        {
           return File.Exists($"{path}\\{fileName}");
        }

        public async Task<List<(string fileName, string pathOrContainer)>> UploadAsync(string path, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(webHostEnvironment.WebRootPath, path);

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }


            List<(string fileName, string path)> datas = new();

            foreach (IFormFile file in files)
            {
                string newFileName = await FileRenameAsync(path, HasFile, file.Name);

                await CopyFileAsync($"{uploadPath}\\{newFileName}", file);

                datas.Add((newFileName, $"{uploadPath}\\{newFileName}"));
            }

        

            //todo uyarıcı bir exception oluşturulacak.
            return null;
        }

        public async Task<bool> CopyFileAsync(string path, IFormFile file)
        {

            try
            {
                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, false);

                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }



        }

    }
}
