using MediatR;
using ProjectApp.Application.Base;
using ProjectApp.Application.Contracts;
using ProjectApp.Application.Features.Comments.ProductComments;
using ProjectApp.Domain.Entities;

namespace ProjectApp.Application.Features.Handlers.ProductHandlers
{
    public class RemoveProductCommandHandler(IRepository<Product> _repository,
                                             IUnitOfWork _unitOfWork) : IRequestHandler<RemoveProductCommand, BaseResult<object>>
    {
        public async Task<BaseResult<object>> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
           
            var product = await _repository.GetByIdAsync(request.Id);

            if(product is null)
            {
                return BaseResult<object>.Fail($"Product with Id {request.Id} not found.");
            }

            _repository.Delete(product);

            var result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success() : BaseResult<object>.Fail("Failed to remove the product.");



        }
    }
}
