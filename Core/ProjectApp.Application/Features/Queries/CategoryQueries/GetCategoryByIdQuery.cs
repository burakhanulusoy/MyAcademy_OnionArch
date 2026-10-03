using MediatR;
using ProjectApp.Application.Base;
using ProjectApp.Application.Features.Results.CategoryResults;

namespace ProjectApp.Application.Features.Queries.CategoryQueries;

public record GetCategoryByIdQuery(int Id):IRequest<BaseResult<GetCategoryByIdQueryResult>>;
