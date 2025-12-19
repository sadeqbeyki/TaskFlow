
namespace TaskFlow.Application.Common.Exceptions;

public class ApplicationValidationException : Exception
{
    public ApplicationValidationException(string message)
        : base(message)
    {
    }
}
