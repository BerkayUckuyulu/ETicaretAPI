using ETicaretAPI.Persistance.Contexts;
using ETİcaretAPI.Application.Repositories.ProductImageFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistance.Repositories.ProductImageFile
{
    public class ProductImageFileWriteRepository : WriteRepository<ETİcaretAPI.Domain.File.ProductImageFile>, IProductImageFileWriteRepository
    {
        public ProductImageFileWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
