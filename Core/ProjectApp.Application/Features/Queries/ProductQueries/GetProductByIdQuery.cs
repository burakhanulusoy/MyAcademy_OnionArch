using MediatR;
using ProjectApp.Application.Base;
using ProjectApp.Application.Features.Results.ProductResults;

namespace ProjectApp.Application.Features.Queries.ProductQueries;

public record GetProductByIdQuery(int Id): IRequest<BaseResult<GetProductByIdQueryResult>>;


