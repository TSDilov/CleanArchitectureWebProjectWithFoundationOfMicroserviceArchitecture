using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.DTOs;
using TaskManager.Application.Exceptions;
using TaskManager.Application.Features.Tasks.Create;
using TaskManager.Application.Features.Tasks.Delete;
using TaskManager.Application.Features.Tasks.GetAll;
using TaskManager.Application.Features.Tasks.GetSingle;
using TaskManager.Application.Features.Tasks.Update;
using TaskManager.Application.Responses;

namespace TaskManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly IMediator mediator;

        public TasksController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserTaskDto>>> Get()
        {
            try
            {
                var userTasks = await this.mediator.Send(new GetUserTaskListRequest());
                return Ok(userTasks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserTaskDto>> GetById(int id)
        {
            try
            {
                var userTasks = await this.mediator.Send(new GetUserTaskDetailsRequest { Id = id });
                return Ok(userTasks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateUserTaskCommand command)
        {
            try
            {
                var response = await this.mediator.Send(command);
                return Ok(response);
            }
            catch (ValidationException ex)
            {
                var errorMessages = new List<string>();
                foreach (var error in ex.Errors)
                {
                    errorMessages.Add(error);
                }
                return BadRequest(errorMessages);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UpdateUserTaskCommand command)
        {
            try
            {
                await this.mediator.Send(command);
                return NoContent();
            }
            catch (ValidationException ex)
            {
                var errorMessages = new List<string>();
                foreach (var error in ex.Errors)
                {
                    errorMessages.Add(error);
                }
                return BadRequest(errorMessages);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var command = new DeleteUserTaskCommand { Id = id };
                await this.mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
