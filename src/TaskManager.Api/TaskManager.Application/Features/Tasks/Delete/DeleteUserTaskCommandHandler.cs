using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Infrastructure;
using TaskManager.Application.Exceptions;
using TaskManager.Domain;

namespace TaskManager.Application.Features.Tasks.Delete
{
    public class DeleteUserTaskCommandHandler : IRequestHandler<DeleteUserTaskCommand>
    {
        private readonly ITaskRepository taskRepository;
        private readonly IMapper mapper;

        public DeleteUserTaskCommandHandler(ITaskRepository taskRepository, IMapper mapper)
        {
            this.taskRepository = taskRepository;
            this.mapper = mapper;
        }
        public async Task Handle(DeleteUserTaskCommand request, CancellationToken cancellationToken)
        {
            var userTask = await taskRepository.Get(request.Id);
            if (userTask == null)
            {
                throw new NotFoundException(nameof(UserTask), request.Id);
            }

            await taskRepository.Delete(userTask);
        }
    }
}
