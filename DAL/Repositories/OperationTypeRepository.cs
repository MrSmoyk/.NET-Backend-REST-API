using DAL.Interfases;
using Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public class OperationTypeRepository : BaseRepositoty<OperationType>, IOperationTypeRepository
    {
        public OperationTypeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<OperationType>> GetAllAsync()
        {
            return await context.Set<OperationType>()
                .AsNoTracking()
                .ToListAsync();
        }

        public override async Task<IEnumerable<OperationType>> GetByStateAsync(Expression<Func<OperationType, bool>> expression)
        {

            return await context.Set<OperationType>()
                .Where(expression)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<OperationType> GetTypeByNameAsync(string name)
        {
            var operationType = await GetByStateAsync(x => x.Name == name);
            return operationType.FirstOrDefault();
        }
    }
}
