using ETİcaretAPI.Application.Abstraction.Storage;
using ETİcaretAPI.Application.Features.Commands.CreateProduct;
using ETİcaretAPI.Application.Features.Commands.Product;
using ETİcaretAPI.Application.Features.Commands.ProductImageFile;
using ETİcaretAPI.Application.Features.Queries.GetAllProduct;
using ETİcaretAPI.Application.Features.Queries.Product;
using ETİcaretAPI.Application.Features.Queries.ProductImageFile;
using ETİcaretAPI.Application.Repositories;
using ETİcaretAPI.Application.Repositories.InvoiceFile;
using ETİcaretAPI.Application.Repositories.ProductImageFile;
using ETİcaretAPI.Application.RequestParameters;
using ETİcaretAPI.Application.Services;
using ETİcaretAPI.Application.ViewModels.Products;
using ETİcaretAPI.Domain;
using ETİcaretAPI.Domain.File;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly IMediator _mediator;


        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);
            return Ok(response);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute]GetByIdProductQueryRequest getByIdProductQueryRequest)
        {
           GetByIdProductQueryResponse getByIdProductQueryResponse= await _mediator.Send(getByIdProductQueryRequest);
           return Ok(getByIdProductQueryResponse);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
             await _mediator.Send(createProductCommandRequest);
                return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateProductCommandRequest updateProductCommandRequest)
        {
            await _mediator.Send(updateProductCommandRequest);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest removeProductCommandRequest)
        {
            await _mediator.Send(removeProductCommandRequest);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromQuery] UploadProductImageCommandRequest uploadProductImageCommandRequest)
        {

            uploadProductImageCommandRequest.Files= Request.Form.Files;

           await  _mediator.Send(uploadProductImageCommandRequest);

            return Ok();

        }

        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetProductImages([FromRoute]GetProductImagesQueryRequest request)
        {
           List<GetProductImagesQueryResponse> getProductImagesQueryResponses= await _mediator.Send(request);
            return Ok(getProductImagesQueryResponses);
        }
        [HttpDelete("[action]/{ProductId}")]
        public async Task<IActionResult> DeleteImage([FromRoute]RemoveProductImageCommandRequest removeProductImageCommandRequest,[FromQuery]string imageId)
        {
            removeProductImageCommandRequest.ImageId = imageId;
           await _mediator.Send(removeProductImageCommandRequest);
 
            return Ok();

        }
    }
}
