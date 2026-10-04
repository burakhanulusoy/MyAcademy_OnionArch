using Mapster;
using MediatR;
using ProjectApp.Application.Base;
using ProjectApp.Application.Contracts;
using ProjectApp.Application.Features.Queries.ProductQueries;
using ProjectApp.Application.Features.Results.ProductResults;
using ProjectApp.Domain.Entities;
using System.Reflection.Metadata;

namespace ProjectApp.Application.Features.Handlers.ProductHandlers
{
    public class GetProductByIdQueryHandler(IRepository<Product> _repository): IRequestHandler<GetProductByIdQuery, BaseResult<GetProductByIdQueryResult>>
    {
        public async Task<BaseResult<GetProductByIdQueryResult>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id);

            if(product is null)
            {
                return BaseResult<GetProductByIdQueryResult>.Fail($"Product with Id {request.Id} not found.");
            }

            var mappedProduct = product.Adapt<GetProductByIdQueryResult>();

            return BaseResult<GetProductByIdQueryResult>.Success(mappedProduct);





        }
    }
}
