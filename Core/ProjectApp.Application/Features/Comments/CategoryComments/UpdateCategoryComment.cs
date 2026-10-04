using MediatR;
using ProjectApp.Application.Base;

namespace ProjectApp.Application.Features.Comments.CategoryComments;

public record UpdateCategoryComment(int Id,string Name):IRequest<BaseResult<object>>;
