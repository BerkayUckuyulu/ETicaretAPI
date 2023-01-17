using System;
using ETİcaretAPI.Application.Repositories;
using ETİcaretAPI.Application.RequestParameters;
using MediatR;

namespace ETİcaretAPI.Application.Features.Queries.GetAllProduct
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;

        public GetAllProductQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
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
            }).Skip(request.Page * request.Size).Take(request.Size).ToList();

            return new()
            {
                Products = products,
                TotalCount = totalCount
            };
 
        }
    }
    public class GetAllProductQueryRequest : IRequest<GetAllProductQueryResponse>
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }

    public class GetAllProductQueryResponse
    {
        public int TotalCount { get; set; }
        public object Products { get; set; }
    }
}

