using FluentValidation;

namespace CaseTecnico.Application.Contracts.Extensions
{
    public static class RequestExtensions
    {
        public static (bool IsValid, object? Errors) Validate<T>(this T request, IValidator<T> validator)
        {
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
                return (false, validationResult.Errors.Select(x => new { x.ErrorMessage }));

            return (true, null);
        }
    }
}
