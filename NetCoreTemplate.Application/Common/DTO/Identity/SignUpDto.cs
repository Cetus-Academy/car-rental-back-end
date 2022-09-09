using FluentValidation;

namespace NetCoreTemplate.Application.Common.DTO.Identity;
public record SignUpDto
{
    public string Email { get; set; }

    public string Password { get; set; }

    public string ConfirmPassword { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class SignUpDtoValidator : AbstractValidator<SignUpDto>
{
    public SignUpDtoValidator()
    {
        RuleFor(a => a.Email)
            .NotEmpty().WithMessage("{PropertyName} cannot be empty.")
            .NotNull().WithMessage("{PropertyName} cannot be empty.")
            .EmailAddress().WithMessage("Invalid form of email address.");

        RuleFor(a => a.Password)
            .NotEmpty().WithMessage("{PropertyName} cannot be empty.")
            .NotNull().WithMessage("{PropertyName} cannot be empty.");

        RuleFor(a => a.ConfirmPassword)
            .NotEmpty().WithMessage("{PropertyName} cannot be empty.")
            .NotNull().WithMessage("{PropertyName} cannot be empty.")
            .Equal(a => a.Password).WithMessage("Passwords need to be the same");
        
        RuleFor(a => a.FirstName)
            .NotEmpty().WithMessage("The field {PropertyName} is required.")
            .NotNull().WithMessage("The field {PropertyName} is required.")
            .MaximumLength(50).WithMessage("The maximum length is 50.");

        RuleFor(a => a.LastName)
            .NotEmpty().WithMessage("The field {PropertyName} is required.")
            .NotNull().WithMessage("The field {PropertyName} is required.")
            .MaximumLength(50).WithMessage("The maximum length is 50.");
        
    }
}