using MediatR;
using TaskManager.Application.DTOs;
using TaskManager.Application.Responses;

namespace TaskManager.Application.Features.Tasks.Update
{
    public class UpdateUserTaskCommand : UserTaskDto, IRequest<BaseCommandResponse>
    {
    }
}
