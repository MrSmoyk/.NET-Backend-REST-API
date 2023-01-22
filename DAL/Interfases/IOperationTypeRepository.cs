using Domain.Entitys;

namespace DAL.Interfases
{
    public interface IOperationTypeRepository : IBaseRepository<OperationType>
    {
        Task<OperationType> GetTypeByNameAsync(string name);
    }
}
