using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreTemplate.API.Attributes;
using NetCoreTemplate.Application.Commands.Reports.Create;
using NetCoreTemplate.Application.Queries.GetReportList;
using NetCoreTemplate.Domain.Identity.Static;
using NetCoreTemplate.Persistence.EF.Context;
using NetCoreTemplate.Persistence.EF.DbContextFactory;
using NetCoreTemplate.Shared.Models;

namespace NetCoreTemplate.API.Controllers;

[ApiAuthorize(Roles = UserRoles.User)]
[Route("reports")]
public class ReportController : BaseController
{
    private readonly ReadDbContext _dbContext;

    public ReportController(ReadDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReportCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
    
    [HttpGet]
    public async Task<ActionResult<PaginatedList<GetReportListQueryVm>>> Get([FromQuery] GetReportListQuery query)
    {
        var response = await Mediator.Send(query);
        return OkOrNotFound(response);
    }
   
}