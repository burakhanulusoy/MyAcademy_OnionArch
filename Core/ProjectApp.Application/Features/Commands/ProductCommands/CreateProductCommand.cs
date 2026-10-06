using MediatR;
using Microsoft.AspNetCore.Http;
using ProjectApp.Application.Base;

namespace ProjectApp.Application.Features.Comments.ProductComments;

public record CreateProductCommand(string? Name,decimal? Price,string? Description,int? CategoryId, IFormFile? Image) :IRequest<BaseResult<object>>;