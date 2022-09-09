using MediatR;
using NetCoreTemplate.Application.Common.DTO;
using NetCoreTemplate.Domain.Reports.Enums;

namespace NetCoreTemplate.Application.Commands.Reports.Create;

public record CreateReportCommand(string Title, string Description, ReportType ReportType, AddressDto Address, LocalizationDto Localization) : IRequest;


