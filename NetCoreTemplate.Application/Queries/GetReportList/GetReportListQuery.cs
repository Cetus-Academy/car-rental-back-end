using MediatR;
using NetCoreTemplate.Shared.Models;

namespace NetCoreTemplate.Application.Queries.GetReportList;

public record GetReportListQuery(string SearchPhrase) : PaginationRequest, IRequest<PaginatedList<GetReportListQueryVm>>;