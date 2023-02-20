namespace CaseTecnico.Domain.ResultObject
{
    public class Error
    {
        public string Codigo { get; set; } = string.Empty;
        public string Mensagem { get; set; } = string.Empty;

        public Error(string codigo, string mensagem)
        {
            Codigo = codigo;
            Mensagem = mensagem;
        }
    }
}
