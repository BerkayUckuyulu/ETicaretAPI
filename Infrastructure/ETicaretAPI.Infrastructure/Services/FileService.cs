using ETicaretAPI.Infrastructure.StaticServices;
using ETİcaretAPI.Application.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services
{
    public class FileService : IFileService
    {
        readonly IWebHostEnvironment webHostEnvironment;
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
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

        private async Task<string> FileRenameAsync(string path, string fileName)
        {
            return await Task.Run<string>(() =>
            {
                string oldName = Path.GetFileNameWithoutExtension(fileName);
                string extension = Path.GetExtension(fileName);
                string newFileName = $"{NameOperation.CharacterRegulatory(oldName)}{extension}";
                bool fileIsExists = false;
                int fileIndex = 1;
                do
                {
                    if (File.Exists($"{path}\\{newFileName}"))
                    {
                        fileIsExists = true;
                        fileIndex++;
                        newFileName = $"{NameOperation.CharacterRegulatory(oldName + "-" + fileIndex)}{extension}";
                    }
                    else
                    {
                        fileIsExists = false;
                    }
                } while (fileIsExists);

                return newFileName;
            });



        }

        public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(webHostEnvironment.WebRootPath, path);

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }


            List<(string fileName, string path)> datas = new();
            List<bool> results = new();

            foreach (IFormFile file in files)
            {
                string fileNewName = await FileRenameAsync(uploadPath,file.FileName);

                bool result = await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);

                datas.Add((fileNewName, $"{uploadPath}\\{fileNewName}"));
                results.Add(result);
            }

            if (results.TrueForAll(r => r.Equals(true)))
            {
                return datas;
            }

            //todo uyarıcı bir exception oluşturulacak.
            return null;

           

        }


    }
}
