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
    public class UpdateProductCommandHandler(IRepository<Product> _repository,
                                             IRepository<Category> _categoryRepository,
                                             IUnitOfWork _unitOfWork,
                                             IValidator<UpdateProductCommand> _validator,
                                             IFileService _fileService) // YENÝ
        : IRequestHandler<UpdateProductCommand, BaseResult<object>>
    {
        public async Task<BaseResult<object>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            // 1. Validasyon
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return BaseResult<object>.Fail(validationResult.Errors);
            }

            // 2. Ürün var mý?
            var existingProduct = await _repository.GetByIdAsync(request.Id);

            if (existingProduct is null)
            {
                return BaseResult<object>.Fail($"{request.Id} numaralý ürün bulunamadý.");
            }

            // 3. Kategori var mý?
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId!.Value);

            if (category is null)
            {
                return BaseResult<object>.Fail($"{request.CategoryId} numaralý kategori bulunamadý.");
            }

            // 4. Yeni deðerleri mevcut ürünün üzerine yaz (ImageUrl'a dokunmaz)
            request.Adapt(existingProduct);

            // Eski görselin yolunu Adapt'ten ÖNCE sakla
            // (Adapt artýk ImageUrl'u da üzerine yazýyor)
            var oldImageUrl = existingProduct.ImageUrl;

            request.Adapt(existingProduct);

            _repository.Update(existingProduct);
            var result = await _unitOfWork.SaveChangesAsync();

            if (!result)
            {
                return BaseResult<object>.Fail("Ürün güncellenirken bir hata oluþtu.");
            }

            // Kayýt baþarýlý ve görsel deðiþtiyse eskisini sil
            if (oldImageUrl != existingProduct.ImageUrl)
            {
                _fileService.Delete(oldImageUrl);
            }

            return BaseResult<object>.Success();
        }
    }
}