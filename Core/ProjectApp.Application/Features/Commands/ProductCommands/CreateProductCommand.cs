using MediatR;
using ProjectApp.Application.Base;

namespace ProjectApp.Application.Features.Comments.ProductComments;

public record CreateProductCommand(string? Name,decimal? Price,string? Description,int? CategoryId):IRequest<BaseResult<object>>;