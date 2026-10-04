using FluentValidation;
using Mapster;
using MediatR;
using ProjectApp.Application.Base;
using ProjectApp.Application.Contracts;
using ProjectApp.Application.Features.Comments.CategoryComments;
using ProjectApp.Domain.Entities;

namespace ProjectApp.Application.Features.Handlers.CategoryHandlers
{
    public class CreateCategoryCommnetHandler(IRepository<Category> _repository,
                                              IUnitOfWork _unitOfWork,
                                              IValidator<CreateCategoryComment> _validator) : IRequestHandler<CreateCategoryComment, BaseResult<object>>
    {
        public async Task<BaseResult<object>> Handle(CreateCategoryComment request, CancellationToken cancellationToken)
        {
           
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            //fast fail.
            if (!validationResult.IsValid)
            {
                return  BaseResult<object>.Fail(validationResult.Errors);
            }

            var mappedCategory = request.Adapt<Category>();

            await _repository.CreateAsync(mappedCategory);
            
            var result =await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success() : BaseResult<object>.Fail("Kategori eklenirken bir sorun oluþtu.");

        }
    }
}
