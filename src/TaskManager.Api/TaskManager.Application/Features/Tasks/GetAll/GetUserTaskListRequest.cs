using MediatR;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Features.Tasks.GetAll
{
    public class GetUserTaskListRequest : IRequest<List<UserTaskDto>>
    {
    }
}
