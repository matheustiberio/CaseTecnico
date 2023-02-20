namespace CaseTecnico.Domain.ResultObject
{
    public static class MensagensErro
    {
        public static readonly Error ErroInterno = new("0", "Erro interno.");

        public static readonly Error EmailJaUtilizado = new("1", "Este e-mail já está em uso.");
        public static readonly Error ClienteNaoEncontrado = new("2", "Cliente não encontrado.");
    }
}
