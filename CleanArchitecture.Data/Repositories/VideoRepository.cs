using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class VideoRepository : RepositoryBase<Video>, IVideoRepository
    {
        public VideoRepository(StreamerDbContext context) : base(context)
        {

        }

        public async Task<Video> GetVideoByNombre(string nombreVideo)
        {
            var video = await context.Videos!.Where(e => e.Nombre == nombreVideo).SingleOrDefaultAsync();

            return video!;
        }

        public async Task<IEnumerable<Video>> GetVideoByUsername(string username)
        {
            var videos = await context.Videos!.Where(e => e.CreatedBy == username).ToListAsync();

            return videos;
        }
    }
}
