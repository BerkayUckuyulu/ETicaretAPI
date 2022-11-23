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
    public class ProductWriteRepository : WriteRepository<Product>, IProductImageFileWriteRepository
    {
        public ProductWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }

        DbSet<ETİcaretAPI.Domain.File.ProductImageFile> IRepository<ETİcaretAPI.Domain.File.ProductImageFile>.Table => throw new NotImplementedException();

        public Task<bool> AddAsync(ETİcaretAPI.Domain.File.ProductImageFile model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddRangeAsync(List<ETİcaretAPI.Domain.File.ProductImageFile> datas)
        {
            throw new NotImplementedException();
        }

        public bool Remove(ETİcaretAPI.Domain.File.ProductImageFile model)
        {
            throw new NotImplementedException();
        }

        public bool RemoveRange(List<ETİcaretAPI.Domain.File.ProductImageFile> datas)
        {
            throw new NotImplementedException();
        }

        public bool Update(ETİcaretAPI.Domain.File.ProductImageFile model)
        {
            throw new NotImplementedException();
        }
    }
}
