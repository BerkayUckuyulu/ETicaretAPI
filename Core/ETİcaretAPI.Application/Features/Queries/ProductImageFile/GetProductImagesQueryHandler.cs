using System;
using ETİcaretAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ETİcaretAPI.Application.Features.Queries.ProductImageFile
{
    public class GetProductImagesQueryHandler : IRequestHandler<GetProductImagesQueryRequest, List<GetProductImagesQueryResponse>>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IConfiguration _configuration;

        public GetProductImagesQueryHandler(IProductReadRepository productReadRepository, IConfiguration configuration)
        {
            _productReadRepository = productReadRepository;
            _configuration = configuration;
        }

        public async Task<List<GetProductImagesQueryResponse>> Handle(GetProductImagesQueryRequest request, CancellationToken cancellationToken)
        {
            Domain.Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));
            return product?.ProductImageFiles.Select(p => new GetProductImagesQueryResponse()
            {
                Path = $"{_configuration["BaseStorageUrl"]}/{p.Path}",
                Name=p.Name,
                Id=p.Id,
            }).ToList();
        }
    }

    public class GetProductImagesQueryRequest:IRequest<List<GetProductImagesQueryResponse>>
    {
        public string Id { get; set; }
    }

    public class GetProductImagesQueryResponse
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public Guid Id { get; set; }
    }
}

