using DAL.Interfases;
using Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public class OperationRepository : BaseRepositoty<Operation>, IOperationRepository
    {
        public OperationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Operation>> GetAllAsync()
        {
            return await context.Set<Operation>()
                .AsNoTracking()
                .Include(x => x.Type)
                .ToListAsync();
        }

        public override async Task<IEnumerable<Operation>> GetByStateAsync(Expression<Func<Operation, bool>> expression)
        {
            return await context.Set<Operation>()
                .Where(expression)
                .Include(x => x.Type)
                .ToListAsync();
        }

        public async Task<IEnumerable<Operation>> GetForDateAsync(DateTime date)
        {
            return await GetByStateAsync(x => x.Created.Date == date.Date);
        }

        public async Task<IEnumerable<Operation>> GetForPeriodAsync(DateTime startDate, DateTime endDate)
        {
            var c = await GetByStateAsync(x => x.Created.Date >= startDate.Date && x.Created.Date <= endDate.Date);
            return c;
        }
    }
}
