namespace CaseTecnico.Application.Models.Entities
{
    public class Cliente : BaseEntity
    {
        public string NomeCompleto { get; set; }

        public string Email { get; set; }

        public Cliente(string nomeCompleto, string email)
        {
            NomeCompleto = nomeCompleto;
            Email = email;
        }
    }
}
