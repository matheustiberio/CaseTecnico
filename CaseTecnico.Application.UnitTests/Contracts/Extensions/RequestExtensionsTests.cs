using CaseTecnico.Application.Contracts.Extensions;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace CaseTecnico.Application.UnitTests.Contracts.Extensions
{
    public class RequestExtensionsTests
    {
        private readonly Mock<IValidator<object>> _validatorMock;
        

        public RequestExtensionsTests()
        {
            _validatorMock = new Mock<IValidator<object>>();
        }

        [Fact]
        public void ValidateRequest_DeveRetornarSucesso_QuandoRequestValido()
        {
            object request = new();

            _validatorMock.Setup(x => x.Validate(request))
                .Returns(new ValidationResult());

            var result = request.ValidateRequest(_validatorMock.Object);

            Assert.True(result.IsValid);
            Assert.Null(result.Errors);
        }

        [Fact]
        public void ValidateRequest_DeveRetornarErros_QuandoRequestInvalido()
        {
            object request = new();

            _validatorMock.Setup(x => x.Validate(request))
                .Returns(new ValidationResult(new List<ValidationFailure>() { new ValidationFailure()}));

            var result = request.ValidateRequest(_validatorMock.Object);

            Assert.False(result.IsValid);
            Assert.NotNull(result.Errors);
            Assert.IsAssignableFrom<IEnumerable<object?>>(result.Errors);
        }
    }
}
