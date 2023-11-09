using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Identity;
using TaskManager.Application.Contracts.Infrastructure;
using TaskManager.Application.Exceptions;
using TaskManager.Application.Responses;

namespace TaskManager.Application.Features.Tasks.Update
{
    public class UpdateUserTaskCommandHandler : IRequestHandler<UpdateUserTaskCommand, BaseCommandResponse>
    {
        private readonly ITaskRepository taskRepository;
        private readonly IMapper mapper;
        private readonly ICurrentUser currentUser;

        public UpdateUserTaskCommandHandler(ITaskRepository taskRepository, IMapper mapper, ICurrentUser currentUser)
        {
            this.taskRepository = taskRepository;
            this.mapper = mapper;
            this.currentUser = currentUser;
        }
        public async Task<BaseCommandResponse> Handle(UpdateUserTaskCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var currentDateTime = DateTime.UtcNow.AddMinutes(-1);
            var validator = new UpdateUserTaskValidator(currentDateTime);
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }
            else
            {
                var userTask = await taskRepository.Get(request.Id);

                if(userTask.User != currentUser.GetCurrentUser())
                {
                    response.Success = false;
                    response.Message = "Unauthorized";
                    response.Id = userTask.Id;
                    
                    return response;
                }

                mapper.Map(request, userTask);
                userTask.StartDateTime = userTask.StartDateTime.ToUniversalTime();
                userTask.EndDateTime = userTask.EndDateTime.ToUniversalTime();

                await taskRepository.Update(userTask);
                response.Success = true;
                response.Message = "Update Successful";
                response.Id = userTask.Id;
            }

            return response;
        }
    }
}
