using FluentValidation;
using Mapster;
using MediatR;
using ProjectApp.Application.Base;
using ProjectApp.Application.Contracts;
using ProjectApp.Application.Features.Comments.ProductComments;
using ProjectApp.Domain.Entities;

namespace ProjectApp.Application.Features.Handlers.ProductHandlers
{
    public class UpdateProductCommandHandler(IRepository<Product> _repository,
                                         IRepository<Category> _categoryRepository,
                                         IUnitOfWork _unitOfWork,
                                         IValidator<UpdateProductCommand> _validator) : IRequestHandler<UpdateProductCommand, BaseResult<object>>
    {
        public async Task<BaseResult<object>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            // 1. Önce veri kontrolü (veritabanýna gitmeden)
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return BaseResult<object>.Fail(validationResult.Errors);
            }

            // 2. Güncellenecek ürün var mý?
            var existingProduct = await _repository.GetByIdAsync(request.Id);

            if (existingProduct is null)
            {
                return BaseResult<object>.Fail($"{request.Id} numaralý ürün bulunamadý.");
            }

            // 3. Yeni kategori var mý?
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId!.Value);

            if (category is null)
            {
                return BaseResult<object>.Fail($"{request.CategoryId} numaralý kategori bulunamadý.");
            }

            // 4. Yeni nesne oluþturmadan, request'teki deðerleri mevcut ürünün üzerine yaz
            request.Adapt(existingProduct);

            _repository.Update(existingProduct);

            var result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success() : BaseResult<object>.Fail("Ürün güncellenirken bir hata oluþtu.");
        }
    }
}
