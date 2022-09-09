using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreTemplate.API.Controllers;

[ApiController]
public class BaseController : Controller
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    
    protected ActionResult<TResult> OkOrNotFound<TResult>(TResult result)
        => result is null ? NotFound() : Ok(result);
}