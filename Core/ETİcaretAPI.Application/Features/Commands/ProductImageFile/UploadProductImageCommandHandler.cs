using System;
using ETİcaretAPI.Application.Abstraction.Storage;
using ETİcaretAPI.Application.Repositories;
using ETİcaretAPI.Application.Repositories.ProductImageFile;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ETİcaretAPI.Application.Features.Commands.ProductImageFile
{
    public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest, UploadProductImageCommandResponse>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IProductWriteRepository _productWriteRepository;
        readonly IProductImageFileWriteRepository productImageFileWriteRepository;
        readonly IStorageService storageService;

        public UploadProductImageCommandHandler(IProductReadRepository productReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository, IProductWriteRepository productWriteRepository, IStorageService storageService)
        {
            _productReadRepository = productReadRepository;
            this.productImageFileWriteRepository = productImageFileWriteRepository;
            _productWriteRepository = productWriteRepository;
            this.storageService = storageService;
        }

        public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            List<(string fileName, string pathOrContainerName)> result = await storageService.UploadAsync("photo-images", request.Files);

            Domain.Product product = await _productReadRepository.GetByIdAsync(request.Id);

            var result2 = await productImageFileWriteRepository.AddRangeAsync(result.Select(x => new Domain.File.ProductImageFile()
            {
                Name = x.fileName,
                Path = x.pathOrContainerName,
                Storage = storageService.StorageName,
                Products = new List<Domain.Product>() { product }
            }).ToList());

            await _productWriteRepository.SaveAsync();

            return new();
        }
    }

    public class UploadProductImageCommandRequest:IRequest<UploadProductImageCommandResponse>
    {
        public string Id { get; set; }
        public IFormFileCollection? Files { get; set; }
    }

    public class UploadProductImageCommandResponse
    {

    }
}

