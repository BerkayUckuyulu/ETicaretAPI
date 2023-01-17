using System;
using ETİcaretAPI.Application.Repositories;
using MediatR;

namespace ETİcaretAPI.Application.Features.Commands.Product
{
    public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommandRequest, RemoveProductCommandResponse>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IProductWriteRepository _productWriteRepository;

        public RemoveProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        public async Task<RemoveProductCommandResponse> Handle(RemoveProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _productWriteRepository.RemoveAsync(request.Id);
            await _productWriteRepository.SaveAsync();

            return new();
        }
    }

    public class RemoveProductCommandRequest:IRequest<RemoveProductCommandResponse>
    {
        public string Id { get; set; }
    }

    public class RemoveProductCommandResponse
    {

    }
}

