using FluentValidation.Results;
using NetCoreTemplate.Shared.Abstractions.Exceptions;

namespace NetCoreTemplate.Application.Common.Exceptions;
public class OwnValidationException : NetCoreTemplateException
{
    public OwnValidationException()
        : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public OwnValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        
    }
    
    public OwnValidationException(IDictionary<string, string[]> failuresDictionary)
        : this()
    {
        Errors = failuresDictionary;
    }

    public IDictionary<string, string[]> Errors { get; }
}