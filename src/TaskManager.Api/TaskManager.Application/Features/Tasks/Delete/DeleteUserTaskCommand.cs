using MediatR;

namespace TaskManager.Application.Features.Tasks.Delete
{
    public class DeleteUserTaskCommand : IRequest
    {
        public int Id { get; set; }
    }
}
