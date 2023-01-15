using ETİcaretAPI.Application.Abstraction.Storage;
using ETİcaretAPI.Application.Repositories;
using ETİcaretAPI.Application.Repositories.InvoiceFile;
using ETİcaretAPI.Application.Repositories.ProductImageFile;
using ETİcaretAPI.Application.RequestParameters;
using ETİcaretAPI.Application.Services;
using ETİcaretAPI.Application.ViewModels.Products;
using ETİcaretAPI.Domain;
using ETİcaretAPI.Domain.File;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
         readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IFileService fileService;
        readonly IFileWriteRepository fileWriteRepository;
        readonly IFileReadRepository fileReadRepository;
        readonly IProductImageFileReadRepository  productImageFileReadRepository;
        readonly IProductImageFileWriteRepository  productImageFileWriteRepository;
        readonly IInvoiceFileWriteRepository ınvoiceFileWriteRepository;
        readonly IInvoiceFileReadRepository ınvoiceFileReadRepository;
        readonly IStorageService storageService;



        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IWebHostEnvironment _webHostEnvironment, IFileService fileService, IFileWriteRepository fileWriteRepository, IFileReadRepository fileReadRepository, IProductImageFileReadRepository productImageFileReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository, IInvoiceFileWriteRepository ınvoiceFileWriteRepository, IInvoiceFileReadRepository ınvoiceFileReadRepository, IStorageService storageService)
        {
            this._productWriteRepository = productWriteRepository;
            this._productReadRepository = productReadRepository;
            this.webHostEnvironment = _webHostEnvironment;
            this.fileService = fileService;
            this.fileWriteRepository = fileWriteRepository;
            this.fileReadRepository = fileReadRepository;
            this.productImageFileReadRepository = productImageFileReadRepository;
            this.productImageFileWriteRepository = productImageFileWriteRepository;
            this.ınvoiceFileWriteRepository = ınvoiceFileWriteRepository;
            this.ınvoiceFileReadRepository = ınvoiceFileReadRepository;
            this.storageService = storageService;
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]Pagination pagination)
        {
            var totalCount = _productReadRepository.GetAll(false).Count();
            var products = _productReadRepository.GetAll(false).Select(p => new
            {
                p.Id,
                p.Name,
                p.CreatedDate,
                p.UpdatedDate,
                p.Stock,
                p.Price
            }).Skip(pagination.Page * pagination.Size).Take(pagination.Size);

            return Ok(new { totalCount,products });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {

            return Ok(await _productReadRepository.GetByIdAsync(id, false));
        }

        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Product model)
        {
            if (ModelState.IsValid)
            {
                var result=await _productWriteRepository.AddAsync(new()
                {
                    Name = model.Name,
                    Price = model.Price,
                    Stock = model.Stock


                });


                var data=await _productWriteRepository.SaveAsync();
                return StatusCode((int)HttpStatusCode.Created);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

         
        }

        [HttpPut]
        public async Task<IActionResult> Put(VM_Update_Product model)
        {
            Product product = await _productReadRepository.GetByIdAsync(model.Id);

            product.Name = model.Name;
            product.Price = model.Price;
            product.Stock = model.Stock;

            await _productWriteRepository.SaveAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload(string id)
        {

            List<(string fileName,string pathOrContainerName)> result= await storageService.UploadAsync("photo-images", Request.Form.Files);

            Product product =await _productReadRepository.GetByIdAsync(id);

            await productImageFileWriteRepository.AddRangeAsync(result.Select(x => new ProductImageFile()
            {
                Name = x.fileName,
                Path = x.pathOrContainerName,
                Storage =storageService.StorageName,
                Products=new List<Product>() { product}
            }).ToList());

            await _productWriteRepository.SaveAsync();
            return Ok();
           
        }
    }
}
