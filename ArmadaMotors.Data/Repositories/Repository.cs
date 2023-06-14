using ArmadaMotors.Data.DbContexts;
using ArmadaMotors.Data.IRepositories;
using ArmadaMotors.Domain.Commons;
using Microsoft.EntityFrameworkCore;

namespace ArmadaMotors.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
    {
        private readonly ArmadaDbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;
        public Repository(ArmadaDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public async ValueTask<bool> DeleteAsync(long id)
        {
            var entity = await this._dbSet.FindAsync(id);
            this._dbSet.Remove(entity);

            return await this._dbContext.SaveChangesAsync() > 0;
        }

        public async ValueTask<TEntity> InsertAsync(TEntity entity)
        {
            var entry = await this._dbSet.AddAsync(entity);
            await this._dbContext.SaveChangesAsync();

            return entry.Entity;
        }

        public IQueryable<TEntity> SelectAll()
        {
            return this._dbSet;
        }

        public async ValueTask<TEntity> SelectByIdAsync(long id)
        {
            return await this._dbSet.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public async ValueTask<TEntity> UpdateAsync(TEntity entity)
        {
            var entry = this._dbSet.Update(entity);
            await this._dbContext.SaveChangesAsync();

            return entry.Entity;
        }
    }
}