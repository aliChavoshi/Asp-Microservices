using FluentValidation.Results;

namespace Application.Exceptions;

public class ValidationException : Exception
{
    public ValidationException() : base("one or more validation error exist")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures.GroupBy(x => x.PropertyName, e => e.ErrorMessage)
            .ToDictionary(f => f.Key, f => f.ToArray());
    }

    public Dictionary<string, string[]> Errors { get; set; }
}