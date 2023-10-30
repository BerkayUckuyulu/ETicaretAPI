using ETicaretAPI.Persistance.Contexts;
using ETİcaretAPI.Application.Repositories;
using ETİcaretAPI.Application.Repositories.ProductImageFile;
using ETİcaretAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistance.Repositories
{
    public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
    {
        public ProductWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }       
    }
}
