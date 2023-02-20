using CaseTecnico.Application.Contracts.Requests;
using FluentValidation;

namespace CaseTecnico.Application.Contracts.Validators
{
    public class AtualizarClienteRequestValidator : AbstractValidator<AtualizarClienteRequest>
    {
        public AtualizarClienteRequestValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress();

            RuleFor(x => x.NomeCompleto)
                .MinimumLength(3);
        }
    }
}
