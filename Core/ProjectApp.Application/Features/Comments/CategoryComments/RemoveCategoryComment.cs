using MediatR;
using ProjectApp.Application.Base;

namespace ProjectApp.Application.Features.Comments.CategoryComments;

public record RemoveCategoryComment(int Id):IRequest<BaseResult<object>>;