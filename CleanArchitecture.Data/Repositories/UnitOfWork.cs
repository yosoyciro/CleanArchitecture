using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain.Commom;
using CleanArchitecture.Infrastructure.Persistence;
using System.Collections;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StreamerDbContext context;
        private Hashtable repositories;
        private IVideoRepository videoRepository;
        private IStreamerRepository streamerRepository;

        //Repositorios especiales no se inyectan, de definen x propiedades
        public IVideoRepository VideoRepository => videoRepository ??= new VideoRepository(context);
        public IStreamerRepository StreamerRepository => streamerRepository ??= new StreamerRepository(context);

        public UnitOfWork(StreamerDbContext context)
        {
            this.context = context;
        }

        public StreamerDbContext StreamerDbContext => context;

        public async Task<int> CommitAsync()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel
        {
            if (repositories == null)
            {
                repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;

            if (!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(RepositoryBase<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), context);
                repositories.Add(type, repositoryInstance);
            }

            return (IAsyncRepository<TEntity>)repositories[type];
        }
    }
}
