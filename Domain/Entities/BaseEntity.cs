namespace CaseTecnico.Application.Models.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public DateTime CriadoEm { get; set; }
    }
}
