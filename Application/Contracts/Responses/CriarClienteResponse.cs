namespace CaseTecnico.Application.Contracts.Responses
{
    public class CriarClienteResponse
    {
        public int Id { get; set; }
        
        public string NomeCompleto { get; set; } = string.Empty;
        
        public CriarClienteResponse(int id, string nomeCompleto)
        {
            Id = id;
            NomeCompleto = nomeCompleto;
        }
    }
}
