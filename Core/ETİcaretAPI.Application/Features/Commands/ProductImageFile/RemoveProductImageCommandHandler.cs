using System;
using ETİcaretAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace ETİcaretAPI.Application.Features.Commands.ProductImageFile
{
    public class RemoveProductImageCommandHandler : IRequestHandler<RemoveProductImageCommandRequest, RemoveProductImageCommandResponse>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IProductWriteRepository _productWriteRepository;

        public RemoveProductImageCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }
        public async Task<RemoveProductImageCommandResponse> Handle(RemoveProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.ProductId));

            var productImage = product?.ProductImageFiles.FirstOrDefault(x => x.Id == Guid.Parse(request.ImageId));

            if (productImage !=null)
            {
                product?.ProductImageFiles.Remove(productImage);
            }
            await _productWriteRepository.SaveAsync();


            return new();
        }
    }

    public class RemoveProductImageCommandRequest:IRequest<RemoveProductImageCommandResponse>
    {
        public string ProductId { get; set; }
        public string? ImageId { get; set; } 

    }

    public class RemoveProductImageCommandResponse
    {

    }
}

