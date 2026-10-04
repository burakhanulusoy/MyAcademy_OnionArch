  using MediatR;
using ProjectApp.Application.Base;

namespace ProjectApp.Application.Features.Comments.CategoryComments;

public record RemoveCategoryCommand(int Id):IRequest<BaseResult<object>>;