using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using NetCoreTemplate.Application.Common.Exceptions;
using NetCoreTemplate.Application.Common.GlobalRegexes;

namespace NetCoreTemplate.Application.Common.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    // IValidator recognizes whether there is created AbstractValidator<TRequest> 
    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        var failures = new List<ValidationFailure>();
        
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            failures = _validators.Select(a => a.Validate(context)).SelectMany(result => result.Errors)
                .Where(b => b != null).ToList();
        }
        
        var requestType = request.GetType();
        var requestStringProperties = requestType.GetProperties().Where(a => a.PropertyType == typeof(string)).ToList();

        if (!requestStringProperties.Any() && failures.Count > 0)
        {
            throw new OwnValidationException(failures);
        }

        if (requestStringProperties.Any())
        {
            var reg = new Regex(GlobalRegex.AlphanumericRegex);
        
            var regexFailuresList = requestStringProperties
                .Where(r => reg.IsMatch(r.GetValue(request)?.ToString() ?? string.Empty) == false)
                .Select(a => new ValidationFailure(a.Name, "Provided chars are not supported."))
                .ToList();
            
            

            var allFailures = failures.Union(regexFailuresList).ToList();
        
            if (allFailures.Count != 0)
            {
                throw new OwnValidationException(allFailures);
            }
        }
        
        
        return await next();
    }
}