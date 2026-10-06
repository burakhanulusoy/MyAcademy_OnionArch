using MediatR;
using ProjectApp.Application.Base;
using ProjectApp.Application.Contracts;
using ProjectApp.Application.Features.Comments.ProductComments;
using ProjectApp.Application.Services.FileServices; // YENÝ
using ProjectApp.Domain.Entities;

namespace ProjectApp.Application.Features.Handlers.ProductHandlers
{
    public class RemoveProductCommandHandler(IRepository<Product> _repository,
                                             IUnitOfWork _unitOfWork,
                                             IFileService _fileService) // YENÝ
        : IRequestHandler<RemoveProductCommand, BaseResult<object>>
    {
        public async Task<BaseResult<object>> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id);

            if (product is null)
            {
                return BaseResult<object>.Fail($"Product with Id {request.Id} not found.");
            }

            _repository.Delete(product);

            var result = await _unitOfWork.SaveChangesAsync();

            if (!result)
            {
                return BaseResult<object>.Fail("Failed to remove the product.");
            }

            // YENÝ: Ürün veritabanýndan silindiyse görselini de diskten sil
            _fileService.Delete(product.ImageUrl);

            return BaseResult<object>.Success();
        }
    }
}