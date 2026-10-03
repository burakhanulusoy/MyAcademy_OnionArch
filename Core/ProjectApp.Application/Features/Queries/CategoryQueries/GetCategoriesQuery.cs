using MediatR;
using ProjectApp.Application.Base;
using ProjectApp.Application.Features.Results.CategoryResults;

namespace ProjectApp.Application.Features.Queries.CategoryQueries;

public record GetCategoriesQuery:IRequest<BaseResult<List<GetCategoriesQueryResult>>>;



