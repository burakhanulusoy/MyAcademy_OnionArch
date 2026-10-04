using MediatR;
using ProjectApp.Application.Base;

namespace ProjectApp.Application.Features.Comments.CategoryComments;

public record CreateCategoryCommand(string? Name):IRequest<BaseResult<object>>;





