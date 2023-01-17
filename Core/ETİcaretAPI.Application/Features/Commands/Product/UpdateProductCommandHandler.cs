using System;
using ETİcaretAPI.Application.Repositories;
using MediatR;

namespace ETİcaretAPI.Application.Features.Commands.Product
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IProductWriteRepository _productWriteRepository;

        public UpdateProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        public  async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            ETİcaretAPI.Domain.Product product = await _productReadRepository.GetByIdAsync(request.Id);

            product.Name = request.Name;
            product.Price = request.Price;
            product.Stock = request.Stock;

            await _productWriteRepository.SaveAsync();

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

