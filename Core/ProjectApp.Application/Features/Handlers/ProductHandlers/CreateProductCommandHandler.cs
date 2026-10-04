using FluentValidation;
using Mapster;
using MediatR;
using ProjectApp.Application.Base;
using ProjectApp.Application.Contracts;
using ProjectApp.Application.Features.Comments.ProductComments;
using ProjectApp.Domain.Entities;

namespace ProjectApp.Application.Features.Handlers.ProductHandlers
{
    public class CreateProductCommandHandler(IRepository<Product> _repository,
                                         IRepository<Category> _categoryRepository,
                                         IUnitOfWork _unitOfWork,
                                         IValidator<CreateProductCommand> _validator) : IRequestHandler<CreateProductCommand, BaseResult<object>>
    {
        public async Task<BaseResult<object>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // 1. Ucuz kontroller: veritabanýna gitmeden format/kurallar
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return BaseResult<object>.Fail(validationResult.Errors);
            }

            // 2. Veritabaný kontrolü: kategori gerçekten var mý?
            //value deme sebebim categoryId? böyle budaa oto inte dönüþmez hata veriri hatayý gidermek için value oto int yapar
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId!.Value);

            if (category is null)
            {
                return BaseResult<object>.Fail($"Id'si {request.CategoryId} olan kategori bulunamadý.");
            }

            // 3. Kayýt
            var mappedProduct = request.Adapt<Product>();

            await _repository.CreateAsync(mappedProduct);

            var result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success() : BaseResult<object>.Fail("Ürün oluþturulamadý.");
        }
    }
}
