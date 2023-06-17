using ArmadaMotors.Domain.Commons;

namespace ArmadaMotors.Data.IRepositories
{
    public interface IRepository<TEntity> where TEntity : Auditable
    {
        ValueTask<TEntity> InsertAsync(TEntity entity);
        ValueTask<TEntity> UpdateAsync(TEntity entity);
        ValueTask<bool> DeleteAsync(long id);
        IQueryable<TEntity> SelectAll();
        ValueTask<TEntity> SelectByIdAsync(long id);
        ValueTask<bool> SaveChangesAsync();
    }
}