using FluentValidation;
using Mapster;
using MediatR;
using ProjectApp.Application.Base;
using ProjectApp.Application.Contracts;
using ProjectApp.Application.Features.Comments.CategoryComments;
using ProjectApp.Domain.Entities;

namespace ProjectApp.Application.Features.Handlers.CategoryHandlers
{
    public class UpdateCategoryCommandHandler(IRepository<Category> _repository,
                                              IUnitOfWork _unitOfWork,
                                              IValidator<UpdateCategoryCommand> _validator) : IRequestHandler<UpdateCategoryCommand, BaseResult<object>>
    {
        public async Task<BaseResult<object>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {

            var existingCategory = await _repository.GetByIdAsync(request.Id);

            if (existingCategory is null)
            {
                return BaseResult<object>.Fail($"{request.Id} numaralý kategori bulunamadý.");
            }


            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if(!validationResult.IsValid)
            {
                return BaseResult<object>.Fail(validationResult.Errors);
            }

            var mappedCategory = request.Adapt<Category>();

            _repository.Update(mappedCategory);

            var result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success() : BaseResult<object>.Fail("Güncelleme baþarýsýz.");

        }
    }
}
