using FluentValidation;
using Mapster;
using MediatR;
using ProjectApp.Application.Base;
using ProjectApp.Application.Contracts;
using ProjectApp.Application.Features.Comments.CategoryComments;
using ProjectApp.Domain.Entities;

namespace ProjectApp.Application.Features.Handlers.CategoryHandlers
{
    public class UpdateCategoryCommnetHandler(IRepository<Category> _repository,
                                              IUnitOfWork _unitOfWork,
                                              IValidator<UpdateCategoryComment> _validator) : IRequestHandler<UpdateCategoryComment, BaseResult<object>>
    {
        public async Task<BaseResult<object>> Handle(UpdateCategoryComment request, CancellationToken cancellationToken)
        {
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
