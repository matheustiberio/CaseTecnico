namespace CaseTecnico.Application.Contracts.Requests
{
    public class AtualizarClienteRequest
    {
        public string NomeCompleto { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }
}
