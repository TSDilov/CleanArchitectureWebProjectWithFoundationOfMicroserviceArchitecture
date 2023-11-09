using FluentValidation;
using TaskManager.Application.DTOs.Validators;

namespace TaskManager.Application.Features.Tasks.Update
{
    public class UpdateUserTaskValidator : AbstractValidator<UpdateUserTaskCommand>
    {
        public UpdateUserTaskValidator(DateTime currentDateTime)
        {
            Include(new UserTaskValidator(currentDateTime));

            RuleFor(x => x.Id)
                .NotNull().WithMessage("{PropertyName} must be present.");
        }
    }
}
