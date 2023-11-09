using MediatR;
using TaskManager.Application.DTOs;
using TaskManager.Application.Responses;

namespace TaskManager.Application.Features.Tasks.Create
{
    public class CreateUserTaskCommand : UserTaskDto, IRequest<BaseCommandResponse>
    {
    }
}
