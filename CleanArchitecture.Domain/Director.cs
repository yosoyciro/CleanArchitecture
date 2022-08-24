using CleanArchitecture.Domain.Commom;

namespace CleanArchitecture.Domain
{
    public class Director : BaseDomainModel
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public int VideoId { get; set; }
        public virtual Video? Video { get; set; }
    }
}
