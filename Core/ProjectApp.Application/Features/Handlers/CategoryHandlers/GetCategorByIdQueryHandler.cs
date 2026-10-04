using Mapster;
using MediatR;
using ProjectApp.Application.Base;
using ProjectApp.Application.Contracts;
using ProjectApp.Application.Features.Queries.CategoryQueries;
using ProjectApp.Application.Features.Results.CategoryResults;
using ProjectApp.Domain.Entities;

namespace ProjectApp.Application.Features.Handlers.CategoryHandlers
{
    public class GetCategorByIdQueryHandler(IRepository<Category> _repository) : IRequestHandler<GetCategoryByIdQuery, BaseResult<GetCategoryByIdQueryResult>>
    {
        public async Task<BaseResult<GetCategoryByIdQueryResult>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
          
            var category = await _repository.GetByIdAsync(request.Id);

            if(category is null)
            {
                return BaseResult<GetCategoryByIdQueryResult>.Fail($"Category with Id {request.Id} not found.");
            }

            var mappedCategory = category.Adapt<GetCategoryByIdQueryResult>();
            
            return BaseResult<GetCategoryByIdQueryResult>.Success(mappedCategory);

        }
    }
}
