using FluentValidation;

namespace TaskManager.Application.DTOs.Validators
{
    public class UserTaskValidator : AbstractValidator<IUserTaskDto>
    {
        public UserTaskValidator(DateTime currentDateTime)
        {
            RuleFor(x => x.Subject)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");

            RuleFor(x => x.StartDateTime.ToUniversalTime())
                .LessThan(x => x.EndDateTime.ToUniversalTime())
                .WithMessage("Start date must be less than the end date");

            RuleFor(x => x.StartDateTime.ToUniversalTime())
                .GreaterThanOrEqualTo(currentDateTime)
                .WithMessage("Start date and time can not be earlier than the current date and time.");
        }
    }
}
