using CleanArchitecture.Domain.Commom;

namespace CleanArchitecture.Domain
{
    public class VideoActor : BaseDomainModel
    {
        public int VideoId { get; set; }
        public int ActorId { get; set; }
    }
}
