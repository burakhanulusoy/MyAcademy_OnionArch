using MediatR;
using ProjectApp.Application.Base;

namespace ProjectApp.Application.Features.Comments.ProductComments;
    public record RemoveProductCommand(int Id):IRequest<BaseResult<object>>;
  
