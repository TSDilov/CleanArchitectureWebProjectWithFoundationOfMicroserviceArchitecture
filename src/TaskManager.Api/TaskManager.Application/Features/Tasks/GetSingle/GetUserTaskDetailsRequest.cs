using MediatR;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Features.Tasks.GetSingle
{
    public class GetUserTaskDetailsRequest : IRequest<UserTaskDto>
    {
        public int Id { get; set; }
    }
}
