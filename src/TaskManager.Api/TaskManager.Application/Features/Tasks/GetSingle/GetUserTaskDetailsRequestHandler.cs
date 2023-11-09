using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Infrastructure;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Features.Tasks.GetSingle
{
    public class GetUserTaskDetailsRequestHandler : IRequestHandler<GetUserTaskDetailsRequest, UserTaskDto>
    {
        private readonly ITaskRepository taskRepository;
        private readonly IMapper mapper;

        public GetUserTaskDetailsRequestHandler(ITaskRepository taskRepository, IMapper mapper)
        {
            this.taskRepository = taskRepository;
            this.mapper = mapper;
        }

        public async Task<UserTaskDto> Handle(GetUserTaskDetailsRequest request, CancellationToken cancellationToken)
        {
            var userTask = await taskRepository.Get(request.Id);
            return mapper.Map<UserTaskDto>(userTask);
        }
    }
}
