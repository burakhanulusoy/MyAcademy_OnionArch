using FluentValidation;
using Mapster;
using MediatR;
using ProjectApp.Application.Base;
using ProjectApp.Application.Contracts;
using ProjectApp.Application.Features.Comments.ProductComments;
using ProjectApp.Application.Services.FileServices; // YENÝ
using ProjectApp.Domain.Entities;

namespace ProjectApp.Application.Features.Handlers.ProductHandlers
{
    public class CreateProductCommandHandler(IRepository<Product> _repository,
                                             IRepository<Category> _categoryRepository,
                                             IUnitOfWork _unitOfWork,
                                             IValidator<CreateProductCommand> _validator,
                                             IFileService _fileService) // YENÝ
        : IRequestHandler<CreateProductCommand, BaseResult<object>>
    {
        public async Task<BaseResult<object>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // 1. Validasyon (görsel kontrolü de burada yapýlýyor)
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return BaseResult<object>.Fail(validationResult.Errors);
            }

            // 2. Kategori var mý?
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId!.Value);

            if (category is null)
            {
                return BaseResult<object>.Fail($"Id'si {request.CategoryId} olan kategori bulunamadý.");
            }

            // 3. Map'le
            var mappedProduct = request.Adapt<Product>();

            // YENÝ: Görseli diske kaydet, dönen kýsa yolu entity'ye yaz
            // Validasyondan geçtiði için Image burada null olamaz, o yüzden ! koyduk
            mappedProduct.ImageUrl = await _fileService.UploadAsync(request.Image!, "Product");

            // 4. Kayýt
            await _repository.CreateAsync(mappedProduct);
            var result = await _unitOfWork.SaveChangesAsync();

            // YENÝ: Veritabanýna yazýlamadýysa diskteki görsel boþta kalmasýn, sil
            if (!result)
            {
                _fileService.Delete(mappedProduct.ImageUrl);
                return BaseResult<object>.Fail("Ürün oluþturulamadý.");
            }

            return BaseResult<object>.Success();
        }
    }
}