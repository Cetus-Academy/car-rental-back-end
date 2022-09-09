using MediatR;
using NetCoreTemplate.Application.Common.Services;
using NetCoreTemplate.Application.Repositories;
using NetCoreTemplate.Domain.Reports.Entities;
using NetCoreTemplate.Domain.ValueObjects;

namespace NetCoreTemplate.Application.Commands.Reports.Create;

public class CreateReportCommandHandler : IRequestHandler<CreateReportCommand>
{
    private readonly IReportRepository _reportRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateService _dateService;

    public CreateReportCommandHandler(IReportRepository reportRepository, ICurrentUserService currentUserService, IDateService dateService)
    {
        _reportRepository = reportRepository;
        _currentUserService = currentUserService;
        _dateService = dateService;
    }
    
    public async Task<Unit> Handle(CreateReportCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId;
        var (street,postalCode, city, houseNumber) = request.Address;
        var address = Address.Create(street, postalCode, city, houseNumber: houseNumber);
        var localization = Localization.Create(request.Localization.Latitude, request.Localization.Longitude);
        
        var report = new Report(request.Title, localization, currentUserId);
        report.SetAddress(address);
        report.SetDescription(request.Description);
        report.SetReportType(request.ReportType);

        await _reportRepository.CreateAsync(report);
        return Unit.Value;
    }
}