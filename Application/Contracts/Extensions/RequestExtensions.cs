using FluentValidation;

namespace CaseTecnico.Application.Contracts.Extensions
{
    public static class RequestExtensions
    {
        public static ValidationRequestResult ValidateRequest<T>(this T request, IValidator<T> validator)
        {
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
                return new ValidationRequestResult(false, validationResult.Errors.Select(x => new { x.ErrorMessage }));

            return new ValidationRequestResult(true, null);
        }
    }

    public class ValidationRequestResult
    {
        public bool IsValid { get; set; }

        public IEnumerable<object>? Errors { get; set; }

        public ValidationRequestResult(bool isValid, IEnumerable<object>? errors)
        {
            IsValid = isValid;
            Errors = errors;
        }
    }
}
