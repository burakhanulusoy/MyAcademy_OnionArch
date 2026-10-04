using Mapster;
using MediatR;
using ProjectApp.Application.Base;
using ProjectApp.Application.Contracts;
using ProjectApp.Application.Features.Queries.ProductQueries;
using ProjectApp.Application.Features.Results.ProductResults;
using ProjectApp.Domain.Entities;

namespace ProjectApp.Application.Features.Handlers.ProductHandlers
{
    public class GetProductsWithCategoryQueryHandler(IRepository<Product> _repository) : IRequestHandler<GetProductsWithCategoryQuery, BaseResult<List<GetProductsWithCategoryQueryResult>>>
    {
        public async Task<BaseResult<List<GetProductsWithCategoryQueryResult>>> Handle(GetProductsWithCategoryQuery request, CancellationToken cancellationToken)
        {

            // lazy loading yaptýk bunun için => Include yapmmamýza gerek yok. Product entity'sinde Category navigation property'i var ve lazy loading ile birlikte geldiði için otomatik olarak category bilgisi de gelir.
            var products = await _repository.GetAllAsync();
            
            var mappedProducts = products.Adapt<List<GetProductsWithCategoryQueryResult>>();

            return BaseResult<List<GetProductsWithCategoryQueryResult>>.Success(mappedProducts);
        }
    }
}
