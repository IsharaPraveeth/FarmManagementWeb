using Microsoft.AspNetCore.Mvc;
using MediatR;
using FarmManagement.Application.Features.Fields.Queries.GetAllFields;
using FarmManagement.Application.Features.Fields.Command.Save;
using FarmManagement.Application.Features.Fields.Command.Edit;
using FarmManagement.Application.Features.Fields.Command.Delete;
using FarmManagement.Application.Features.Fields.Queries.GetFieldByName;

namespace FarmManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FieldController : ControllerBase
    {
        private readonly IMediator mediator;

        public FieldController(IMediator _mediator)
        {
            mediator = _mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await mediator.Send(new GetAllFieldsQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] SaveCommand command)
        {
            var result = await mediator.Send(command);

            if (result > 0)
                return Ok(new { Message = "Data saved successfully", Id = result });

            return BadRequest("Failed to save data.");
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] EditCommand command)
        {
            var result = await mediator.Send(command);

            if (result)
                return Ok(new { Message = "Entity updated successfully" });

            return NotFound(new { Message = "Entity not found" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await mediator.Send(new DeleteCommand { Id = id });

            if (result)
                return Ok(new { Message = "Entity deleted successfully" });

            return NotFound(new { Message = "Entity not found" });
        }

        [HttpGet("{name}")]
        public async Task<ActionResult> GetFieldByName(string name)
        {
            var query = new GetFieldByNameQuery(name);
            var result = await mediator.Send(query);

            bool nameExist = false;

            if (result != null)
                  nameExist = true;  

            return Ok(nameExist);
        }
    }
}
