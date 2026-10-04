using MediatR;
using ProjectApp.Application.Base;

namespace ProjectApp.Application.Features.Comments.CategoryComments;

public record CreateCategoryComment(string? Name):IRequest<BaseResult<object>>;





