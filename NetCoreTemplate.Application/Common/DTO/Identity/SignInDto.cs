using FluentValidation;

namespace NetCoreTemplate.Application.Common.DTO.Identity;
public record SignInDto
{
    public string Email { get; set; }
    public string Password { get; set; }

    public bool RememberMe { get; set; }
}

public class SignInDtoValidator : AbstractValidator<SignInDto>
{
    public SignInDtoValidator()
    {
        RuleFor(a => a.Email)
            .NotEmpty().WithMessage("{PropertyName} cannot be empty.")
            .NotNull().WithMessage("{PropertyName} cannot be empty.")
            .EmailAddress().WithMessage("Invalid form of email address.");

        RuleFor(a => a.Password)
            .NotEmpty().WithMessage("{PropertyName} cannot be empty.")
            .NotNull().WithMessage("{PropertyName} cannot be empty.");

    }
}