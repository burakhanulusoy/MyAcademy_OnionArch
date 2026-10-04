using MediatR;
using ProjectApp.Application.Base;
using ProjectApp.Application.Features.Results.ProductResults;

namespace ProjectApp.Application.Features.Queries.ProductQueries;

public record GetProductsWithCategoryQuery:IRequest<BaseResult<List<GetProductsWithCategoryQueryResult>>>;
