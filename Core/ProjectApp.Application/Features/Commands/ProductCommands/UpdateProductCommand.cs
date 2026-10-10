using MediatR;
using Microsoft.AspNetCore.Http;
using ProjectApp.Application.Base;

namespace ProjectApp.Application.Features.Comments.ProductComments;

public record UpdateProductCommand(int Id,
                                   string? Name,
                                   decimal? Price,
                                   string? Description,
                                   int? CategoryId,
                                   string? ImageUrl) : IRequest<BaseResult<object>>;