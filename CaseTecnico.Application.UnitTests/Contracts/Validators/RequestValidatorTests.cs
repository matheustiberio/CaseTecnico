using CaseTecnico.Application.Contracts.Requests;
using CaseTecnico.Application.Contracts.Validators;
using FluentValidation;

namespace CaseTecnico.Application.UnitTests.Contracts.Validators
{
    public class RequestValidatorTests
    {
        private readonly CriarClienteRequestValidator _criarClienteValidator;
        private readonly AtualizarClienteRequestValidator _atualizarClienteValidator;

        public RequestValidatorTests()
        {
            _criarClienteValidator = new CriarClienteRequestValidator();
            _atualizarClienteValidator = new AtualizarClienteRequestValidator();
        }

        [Fact]
        public void CriarClienteValidator_DeveRetornarSucesso_QuandoDadosValidos()
        {
            var request = new CriarClienteRequest
            {
                Email = "teste@teste.com",
                NomeCompleto = "Teste da Silva"
            };

            var validationResult = _criarClienteValidator.Validate(request);

            Assert.True(validationResult.IsValid);
        }

        [Fact]
        public void CriarClienteValidator_DeveRetornarErro_QuandoEmailInvalido()
        {
            var request = new CriarClienteRequest
            {
                Email = "string",
                NomeCompleto = "Teste da Silva"
            };

            var validationResult = _criarClienteValidator.Validate(request);

            Assert.False(validationResult.IsValid);
            Assert.Equal(nameof(request.Email), validationResult.Errors[0].PropertyName);
        }

        [Fact]
        public void CriarClienteValidator_DeveRetornarErro_QuandoNomeCompletoInvalido()
        {
            var request = new CriarClienteRequest
            {
                Email = "teste@teste.com",
                NomeCompleto = "A"
            };

            var validationResult = _criarClienteValidator.Validate(request);

            Assert.False(validationResult.IsValid);
            Assert.Equal(nameof(request.NomeCompleto), validationResult.Errors[0].PropertyName);
        }

        [Fact]
        public void AtualizarClienteValidator_DeveRetornarSucesso_QuandoDadosValidos()
        {
            var request = new AtualizarClienteRequest
            {
                Email = "teste@teste.com",
                NomeCompleto = "Teste da Silva"
            };

            var validationResult = _atualizarClienteValidator.Validate(request);

            Assert.True(validationResult.IsValid);
        }

        [Fact]
        public void AtualizarClienteValidator_DeveRetornarErro_QuandoEmailInvalido()
        {
            var request = new AtualizarClienteRequest
            {
                Email = "string",
                NomeCompleto = "Teste da Silva"
            };

            var validationResult = _atualizarClienteValidator.Validate(request);

            Assert.False(validationResult.IsValid);
            Assert.Equal(nameof(request.Email), validationResult.Errors[0].PropertyName);
        }

        [Fact]
        public void AtualizarClienteValidator_DeveRetornarErro_QuandoNomeCompletoInvalido()
        {
            var request = new AtualizarClienteRequest
            {
                Email = "teste@teste.com",
                NomeCompleto = "A"
            };

            var validationResult = _atualizarClienteValidator.Validate(request);

            Assert.False(validationResult.IsValid);
            Assert.Equal(nameof(request.NomeCompleto), validationResult.Errors[0].PropertyName);
        }
    }
}
