
using JWTAuth.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace JWTAuth.Data
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : BaseEntity, IEntity, new()
    {
        protected readonly AppEfDbContext Context;
        private DbSet<TEntity> entities;

        public BaseRepository(AppEfDbContext dbContext)
        {
            this.Context = dbContext;
            this.entities = Context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await entities.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            entities.Remove(entity);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return await entities.Where(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return await (filter == null ? entities.ToListAsync() : entities.Where(filter).ToListAsync());
        }

        public void Update(TEntity entity)
        {
            entities.Update(entity);
        }
    }
}
