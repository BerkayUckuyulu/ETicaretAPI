using ETicaretAPI.Persistance.Contexts;
using ETİcaretAPI.Application.Repositories.InvoiceFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistance.Repositories.InvoiceFile
{
    public class InvoiceFileWriteRepository : WriteRepository<ETİcaretAPI.Domain.File.InvoiceFile> , IInvoiceFileWriteRepository
    {
        public InvoiceFileWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
