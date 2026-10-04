using MediatR;
using ProjectApp.Application.Base;
using ProjectApp.Application.Contracts;
using ProjectApp.Application.Features.Comments.CategoryComments;
using ProjectApp.Domain.Entities;

namespace ProjectApp.Application.Features.Handlers.CategoryHandlers
{
    public class RemoveCategoryCommnetHandler(IRepository<Category> _repository,
                                              IUnitOfWork _unitOfWork) : IRequestHandler<RemoveCategoryComment, BaseResult<object>>
    {
        public async Task<BaseResult<object>> Handle(RemoveCategoryComment request, CancellationToken cancellationToken)
        {
            
            var category = await _repository.GetByIdAsync(request.Id);

            if(category is null)
            {
                return BaseResult<object>.Fail($"{request.Id} numaralý kategori bulunamadý.");
            }

            _repository.Delete(category);
             
            var result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success() : BaseResult<object>.Fail("Kategori silinirken bir hata oluþtu.");




        }
    }
}
