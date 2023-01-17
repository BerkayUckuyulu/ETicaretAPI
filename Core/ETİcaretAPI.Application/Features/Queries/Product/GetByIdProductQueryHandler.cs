using System;
using ETİcaretAPI.Application.Repositories;
using ETİcaretAPI.Domain;
using MediatR;

namespace ETİcaretAPI.Application.Features.Queries.Product
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {
        readonly IProductReadRepository _productReadRepository;

        public GetByIdProductQueryHandler(IProductReadRepository productReadRepository)
        {
            this._productReadRepository = productReadRepository;
        }

        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
           ETİcaretAPI.Domain.Product product = await _productReadRepository.GetByIdAsync(request.Id, false);
            return new()
            {
                Name=product.Name,
                Price=product.Price,
                Stock=product.Stock
            };
        }
    }

    public class GetByIdProductQueryRequest:IRequest<GetByIdProductQueryResponse>
    {
        public string Id { get; set; }
    }

    public class GetByIdProductQueryResponse
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
    }
}
