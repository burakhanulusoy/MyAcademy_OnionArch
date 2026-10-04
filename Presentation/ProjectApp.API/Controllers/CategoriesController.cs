using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectApp.Application.Features.Comments.CategoryComments;
using ProjectApp.Application.Features.Queries.CategoryQueries;

namespace ProjectApp.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(IMediator _mediator) : ControllerBase
    {
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var results = await _mediator.Send(new GetCategoriesQuery());
            return results.IsSuccessful ? Ok(results) : BadRequest(results);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryComment comment)
        {
            var result= await _mediator.Send(comment);
            return result.IsSuccessful ? Ok(result) : BadRequest(result);
            //return result.IsSuccessful ? Created() : BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCategoryComment comment)
        {
            var result = await _mediator.Send(comment);
            return result.IsSuccessful ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new RemoveCategoryComment(id));
            return result.IsSuccessful ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetCategoryByIdQuery(id));
            return result.IsSuccessful ? Ok(result) : BadRequest(result);
        }



        }
}
