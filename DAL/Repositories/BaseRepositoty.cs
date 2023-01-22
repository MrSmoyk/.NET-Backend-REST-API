using DAL.Interfases;
using Domain.Entitys;
using Domain.Exceptions;
using Domain.Resourses;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public abstract class BaseRepositoty<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ApplicationDbContext context;

        private bool disposing;

        protected BaseRepositoty(ApplicationDbContext _context)
        {
            context = _context;
        }

        public abstract Task<IEnumerable<TEntity>> GetAllAsync();
        public abstract Task<IEnumerable<TEntity>> GetByStateAsync(Expression<Func<TEntity, bool>> expression);

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            var entity = await GetByStateAsync(x => x.Id == id);

            if (!entity.Any())
            {
                throw new EntityNotFoundException(string.Format(ErrorMessageResources.EntityWithIdNotFoundError,id));
            }

            return entity.FirstOrDefault();
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            if (entity is null)
            {
                throw new SourceEntityNullException(ErrorMessageResources.SourceEntityNullError);
            }

            var entityToCreate = await context.Set<TEntity>().AddAsync(entity);
            await SaveChangesAsync();

            return await GetByIdAsync(entityToCreate.Entity.Id);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity is null)
            {
                throw new SourceEntityNullException(ErrorMessageResources.SourceEntityNullError);
            }
            if (!await ExistsAsync(entity.Id))
            {
                throw new EntityNotFoundException(string.Format(ErrorMessageResources.EntityWithIdNotFoundError, entity.Id));
            }

            var entityToUpdate = context.Set<TEntity>().Update(entity);
            await SaveChangesAsync();

            return await GetByIdAsync(entityToUpdate.Entity.Id);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            context.Set<TEntity>().Remove(entity);
            await SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await context.Set<TEntity>().AnyAsync(x => x.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async void Dispose()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }

        private async Task DisposeAsync(bool _disposing)
        {
            if (!disposing)
            {
                if (_disposing)
                {
                    await context.DisposeAsync();
                }
            }

            disposing = true;
        }
    }
}
