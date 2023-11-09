using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Identity;
using TaskManager.Application.Contracts.Infrastructure;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Features.Tasks.GetAll
{
    public class GetUserTaskListRequestHandler : IRequestHandler<GetUserTaskListRequest, List<UserTaskDto>>
    {
        private readonly ITaskRepository taskRepository;
        private readonly IMapper mapper;
        private readonly ICurrentUser currentUser;

        public GetUserTaskListRequestHandler(ITaskRepository taskRepository, IMapper mapper, ICurrentUser currentUser)
        {
            this.taskRepository = taskRepository;
            this.mapper = mapper;
            this.currentUser = currentUser;
        }
        public async Task<List<UserTaskDto>> Handle(GetUserTaskListRequest request, CancellationToken cancellationToken)
        {
            var userTasks = await taskRepository.GetByUser(this.currentUser.GetCurrentUser(), cancellationToken);
            return mapper.Map<List<UserTaskDto>>(userTasks);
        }
    }
}
