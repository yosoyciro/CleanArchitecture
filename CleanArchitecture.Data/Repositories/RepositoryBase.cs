using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain.Commom;
using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : BaseDomainModel
    {
        public StreamerDbContext context;

        public RepositoryBase(StreamerDbContext context)
        {
            this.context = context;
        }

        public async Task<T> AddAsync(T Entity)
        {
            context.Set<T>().Add(Entity);
            await context.SaveChangesAsync();

            return Entity;
        }

        public async Task DeleteAsync(T Entity)
        {
            context.Set<T>().Remove(Entity);
            await context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            IQueryable<T> query = context.Set<T>();
            if (disableTracking) query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString))
            {
                query.Include(includeString);
            }

            if (predicate != null) query.Where(predicate);

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true)
        {
            IQueryable<T> query = context.Set<T>();
            if (disableTracking) query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));
            

            if (predicate != null) query.Where(predicate);

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<T> UpdateAsync(T Entity)
        {
            context.Set<T>().Attach(Entity);
            context.Entry(Entity).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return Entity;
        }

        public void AddEntity(T Entity)
        {
            context.Set<T>().Add(Entity);
        }

        public void DeleteEntity(T Entity)
        {
            context.Set<T>().Attach(Entity);
            context.Entry(Entity).State = EntityState.Modified;
        }

        public void UpdateEntity(T Entity)
        {
            context.Set<T>().Remove(Entity);
        }
    }
}
