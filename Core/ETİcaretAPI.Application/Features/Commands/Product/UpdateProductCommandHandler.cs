using System;
using ETİcaretAPI.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ETİcaretAPI.Application.Features.Commands.Product
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IProductWriteRepository _productWriteRepository;
        readonly ILogger<UpdateProductCommandHandler> _logger;

        public UpdateProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, ILogger<UpdateProductCommandHandler> logger)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _logger = logger;
        }

        public  async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            ETİcaretAPI.Domain.Product product = await _productReadRepository.GetByIdAsync(request.Id);

            product.Name = request.Name;
            product.Price = request.Price;
            product.Stock = request.Stock;

            await _productWriteRepository.SaveAsync();
            _logger.LogInformation("Product Güncellendi...");
            return new();
        }
    }
    public class UpdateProductCommandRequest:IRequest<UpdateProductCommandResponse>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
    }
    public class UpdateProductCommandResponse
    {

    }

}

