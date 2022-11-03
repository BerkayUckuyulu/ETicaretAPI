using ETİcaretAPI.Application.Repositories;
using ETİcaretAPI.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IOrderWriteRepository _orderWriteRepository;
        private readonly ICustomerWriteRepository _customerWriteRepository;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IOrderWriteRepository orderWriteRepository, ICustomerWriteRepository customerWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _orderWriteRepository = orderWriteRepository;
            _customerWriteRepository = customerWriteRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var customerId=Guid.NewGuid();
           await _customerWriteRepository.AddAsync(new() { Id = customerId, Name = "Berkay" });
          await  _orderWriteRepository.AddAsync(new() { Description = "xxdxzc", Address = "cadss",CustomerId=customerId});

            await _orderWriteRepository.SaveAsync();

            return Ok();
               
           
        }
    
    }
}
