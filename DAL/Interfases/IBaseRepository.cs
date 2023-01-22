using System.Linq.Expressions;

namespace DAL.Interfases
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetByStateAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> GetByIdAsync(Guid id);
        Task<TEntity> CreateAsync(TEntity item);
        Task<TEntity> UpdateAsync(TEntity item);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task SaveChangesAsync();
    }
}
