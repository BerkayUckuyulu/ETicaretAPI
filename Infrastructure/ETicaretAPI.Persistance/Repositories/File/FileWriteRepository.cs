using ETicaretAPI.Persistance.Contexts;
using ETİcaretAPI.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistance.Repositories.File
{
    public class FileWriteRepository : WriteRepository<ETİcaretAPI.Domain.File.File>,IFileWriteRepository
    {
        public FileWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
