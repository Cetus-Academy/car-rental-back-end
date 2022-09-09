namespace NetCoreTemplate.Shared.Abstractions.Exceptions;

public abstract class NetCoreTemplateException : Exception
{
    public NetCoreTemplateException(string message) : base(message)
    {
        
    }
}