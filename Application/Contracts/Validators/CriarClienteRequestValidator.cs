using CaseTecnico.Application.Contracts.Requests;
using FluentValidation;

namespace CaseTecnico.Application.Contracts.Validators
{
    public class CriarClienteRequestValidator : AbstractValidator<CriarClienteRequest>
    {
        public CriarClienteRequestValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress();

            RuleFor(x => x.NomeCompleto)
                .MinimumLength(3);
        }
    }
}
