using FluentValidation;
using TaskManager.Application.DTOs.Validators;

namespace TaskManager.Application.Features.Tasks.Create
{
    public class CreateUserTaskValidator : AbstractValidator<CreateUserTaskCommand>
    {
        public CreateUserTaskValidator(DateTime currentDateTime)
        {
            Include(new UserTaskValidator(currentDateTime));
        }
    }
}
