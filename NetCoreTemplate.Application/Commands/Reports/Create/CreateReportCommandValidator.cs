using FluentValidation;

namespace NetCoreTemplate.Application.Commands.Reports.Create;

public class CreateReportCommandValidator : AbstractValidator<CreateReportCommand>
{
    public CreateReportCommandValidator()
    {

        RuleFor(a => a.Description)
            .MaximumLength(1000).WithMessage("You have exceeded maximum length.")
            .NotEmpty().WithMessage("Description can not be empty.")
            .NotNull().WithMessage("Description can not be empty.");
        
        RuleFor(a => a.Title)
            .MaximumLength(500).WithMessage("You have exceeded maximum length.")
            .NotEmpty().WithMessage("Title can not be empty.")
            .NotNull().WithMessage("Title can not be empty.");

        RuleFor(a => a.ReportType)
            .IsInEnum().WithMessage("Provide valid value.");

    }
}