using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectApp.Application.Features.Comments.ProductComments;
using ProjectApp.Application.Features.Queries.ProductQueries;
using System.Threading.Tasks;

namespace ProjectApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IMediator _mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccessful ? Ok(result) : BadRequest(result);
        }

        [HttpPut("id")]
        public async Task<IActionResult> Update(UpdateProductCommand command)
        {
            var result= await _mediator.Send(command);
            return result.IsSuccessful ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var result = await _mediator.Send(new RemoveProductCommand(id));
            return result.IsSuccessful ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result =await _mediator.Send(new GetProductByIdQuery(id));
            return result.IsSuccessful ? Ok(result) : NotFound(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetProductsWithCategoryQuery());
            return result.IsSuccessful ? Ok(result) : NotFound(result);
        }



    }
}
