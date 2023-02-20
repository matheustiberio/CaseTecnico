using FluentValidation;

namespace CaseTecnico.Application.Contracts.Extensions
{
    public static class RequestExtensions
    {
        public static ValidationResult Validate<T>(this T request, IValidator<T> validator)
        {
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
                return new ValidationResult(false, validationResult.Errors.Select(x => new { x.ErrorMessage }));

            return new ValidationResult(true, null);
        }
    }

    public class ValidationResult
    {
        public bool IsValid { get; set; }
        
        public IEnumerable<object>? Errors { get; set; }

        public ValidationResult(bool isValid, IEnumerable<object>? errors)
        {
            IsValid = isValid;
            Errors = errors;
        }
    }
}
