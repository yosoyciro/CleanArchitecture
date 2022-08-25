using FluentValidation.Results;

namespace CleanArchitecture.Application.Exceptions
{
    public class RequestValidationException : ApplicationException
    {
        public RequestValidationException() : base("Se presentaron uno o mas errores de validación")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public RequestValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}
