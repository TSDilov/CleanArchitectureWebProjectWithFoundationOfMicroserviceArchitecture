using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Identity;
using TaskManager.Application.Contracts.Infrastructure;
using TaskManager.Application.Exceptions;
using TaskManager.Application.Features.Tasks.Create;
using TaskManager.Application.Responses;
using TaskManager.Domain;

namespace TaskManager.Application.Features.Handlers
{
    public class CreateUserTaskCommandHandler : IRequestHandler<CreateUserTaskCommand, BaseCommandResponse>
    {
        private readonly ITaskRepository taskRepository;
        private readonly IMapper mapper;
        private readonly ICurrentUser currentUser;

        public CreateUserTaskCommandHandler(ITaskRepository taskRepository, IMapper mapper, ICurrentUser currentUser)
        {
            this.taskRepository = taskRepository;
            this.mapper = mapper;
            this.currentUser = currentUser;
        }
        public async Task<BaseCommandResponse> Handle(CreateUserTaskCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var currentDateTime = DateTime.UtcNow.AddMinutes(-1);
            var validator = new CreateUserTaskValidator(currentDateTime);
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }
            else
            {
                var userTask = mapper.Map<UserTask>(request);
                userTask.User = currentUser.GetCurrentUser();
                userTask.StartDateTime = userTask.StartDateTime.ToUniversalTime();
                userTask.EndDateTime = userTask.EndDateTime.ToUniversalTime();
                userTask = await taskRepository.Add(userTask);
                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = userTask.Id;
            }

            return response;
        }
    }
}
