using MediatR;
using Microsoft.EntityFrameworkCore;
using NetCoreTemplate.Application.Common.DTO;
using NetCoreTemplate.Application.Common.DTO.Identity;
using NetCoreTemplate.Application.Common.Services;
using NetCoreTemplate.Application.Queries.GetReportList;
using NetCoreTemplate.Domain.Common.Enums;
using NetCoreTemplate.Domain.Reports.Entities;
using NetCoreTemplate.Persistence;
using NetCoreTemplate.Persistence.EF.Context;
using NetCoreTemplate.Persistence.EF.Models;
using NetCoreTemplate.Shared.Extensions;
using NetCoreTemplate.Shared.Models;

namespace NetCoreTemplate.Infrastructure.Queries.Reports;

public class GetReportListQueryHandler : IRequestHandler<GetReportListQuery, PaginatedList<GetReportListQueryVm>>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserService _userService;
    private readonly DbSet<ReportReadModel> _reports;

    public GetReportListQueryHandler(ReadDbContext context, ICurrentUserService currentUserService, IUserService userService)
    {
        _currentUserService = currentUserService;
        _userService = userService;
        _reports = context.Reports;
    }

    public async Task<PaginatedList<GetReportListQueryVm>> Handle(GetReportListQuery request,
        CancellationToken cancellationToken)
    {
        var query = _reports.AsNoTracking().Where(r => r.EntryStatus == EntryStatus.Active);
        if (!string.IsNullOrEmpty(request.SearchPhrase))
        {
            query = query.Where(a => EF.Functions.ILike(a.Description, $"%{request.SearchPhrase}%"));
        }

        var vm = await query
            .Where(q => q.UserId == _currentUserService.UserId)
            .Include(q => q.User)
            .Select(a => new GetReportListQueryVm
            {
                Id = a.Id,
                Description = a.Description,
                Title = a.Title,
                ReportDate = a.CreatedAt,
                Localization = a.Localization == null
                    ? null
                    : new LocalizationDto(a.Localization.Latitude, a.Localization.Longitude),
                Address = a.Address == null
                    ? null
                    : new AddressDto(a.Address.Street, a.Address.PostalCode, a.Address.City, a.Address.HouseNumber),
                User = new UserDto
                {
                    Id = a.User.Id,
                    FirstName = a.User.FirstName,
                    LastName = a.User.LastName
                },

                ReportType = a.ReportType
            })
            .PaginatedListAsync(request);

        foreach (var item in vm.Items)
        {
            item.User.Roles = await _userService.GetUserRolesAsync(item.User.Id);
        }
        return vm;
    }
}