using ETicaretAPI.Persistance.Contexts;
using ETİcaretAPI.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistance.Repositories.File
{
    public class FileReadRepository : ReadRepository<ETİcaretAPI.Domain.File.File>,IFileReadRepository
    {
        public FileReadRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
