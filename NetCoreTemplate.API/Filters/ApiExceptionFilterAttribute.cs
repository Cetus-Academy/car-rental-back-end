using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NetCoreTemplate.Application.Common.Exceptions;
using NetCoreTemplate.Domain.Identity.Static;
using NetCoreTemplate.Persistence;
using NetCoreTemplate.Persistence.EF.Context;
using NetCoreTemplate.Shared.Abstractions.Exceptions;

namespace NetCoreTemplate.API.Filters;

//It would be useful to refactor this a little
public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

    public ApiExceptionFilterAttribute()
    {
        // Register known exception types and handlers.
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
        {
            { typeof(OwnValidationException), HandleValidationException },
            { typeof(IdentityErrorException), HandleIdentityException },
            { typeof(UserLockedOutException), HandleUserLockoutException },
            { typeof(NotFoundException), HandleNotFoundException },
            { typeof(CreateUserException), HandleCreateUserException },
            { typeof(ReadDbContext), HandleNetCoreTemplateException },
        };
    }

    public override void OnException(ExceptionContext context)
    {
        HandleException(context);

        base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        Type type = context.Exception.GetType();
        if (_exceptionHandlers.ContainsKey(type))
        {
            _exceptionHandlers[type].Invoke(context);
            return;
        }

        if (!context.ModelState.IsValid)
        {
            HandleInvalidModelStateException(context);
            return;
        }
        
        if (context.Exception is ForbiddenAccessException)
        {
            HandleForbiddenAccessException(context);
            return;
        }

        if (context.Exception is NetCoreTemplateException)
        {
            HandleNetCoreTemplateException(context);
            return;
        }

        HandleUnknownException(context);
    }

    private void HandleNetCoreTemplateException(ExceptionContext context)
    {
        var exception = context.Exception as NetCoreTemplateException;

        var details = new ProblemDetails()
        {
            Title = exception?.Message
        };

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }
    
    private void HandleForbiddenAccessException(ExceptionContext context)
    {
        var exception = context.Exception as NetCoreTemplateException;

        var details = new ProblemDetails()
        {
            Title = exception?.Message
        };

        context.Result = new ForbidResult();

        context.ExceptionHandled = true;
    }

    private void HandleValidationException(ExceptionContext context)
    {
        var exception = context.Exception as OwnValidationException;

        var details = new ValidationProblemDetails(exception?.Errors);

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }
    
    private void HandleIdentityException(ExceptionContext context)
    {
        var exception = context.Exception as IdentityErrorException;

        var details = new
        {
            Title = exception.Message,
            Status = 400,
            Errors = exception.Errors
        };

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }
    
    private void HandleUserLockoutException(ExceptionContext context)
    {
        var exception = context.Exception as UserLockedOutException;

        var details = new
        {
            Title = "Account lockout",
            Status = 400,
            ReasonWhy = exception.ReasonWhy,
            LockoutEnd = exception.LockoutEnd
        };

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }

    private void HandleCreateUserException(ExceptionContext context)
    {
        var exception = context.Exception as CreateUserException;
        
        

        var details = new ValidationProblemDetails(exception?.Errors)
        {
            Title = exception?.Message
        };

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }

    private void HandleInvalidModelStateException(ExceptionContext context)
    {
        var details = new ValidationProblemDetails(context.ModelState);

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }

    private void HandleNotFoundException(ExceptionContext context)
    {
        var exception = context.Exception as NotFoundException;

        var details = new ProblemDetails()
        {
            Title = exception?.Message
        };

        context.Result = new NotFoundObjectResult(details);

        context.ExceptionHandled = true;
    }


    private void HandleUnknownException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "An error occurred while processing your request.",
        };
        
        var isInDevRoles = context.HttpContext.User.IsInRole(UserRoles.Dev);
        
        if (isInDevRoles)
        {
            details.Detail = $"{context.Exception.Message} {context.Exception.Source} {context.Exception.StackTrace}";
        } 
        
        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };

        context.ExceptionHandled = true;
    }
}