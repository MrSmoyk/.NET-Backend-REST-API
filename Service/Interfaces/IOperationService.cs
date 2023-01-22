using Domain.DTO;
using Domain.DTO.OperationDTOs;
using Domain.Entitys;

namespace Service.Interfaces
{
    public interface IOperationService : IBaseService<Operation, OperationDTO>
    {
        public Task<OperationDTO> CreateOperationAsync(OperationCreateUpdateDTO operationDTO);
        public Task<OperationDTO> UpdateOperationAsync(Guid id, OperationCreateUpdateDTO operationDTO);
        public Task<TotalForPeriodDTO> GetTotalForDateAsync(DateTime date);
        public Task<TotalForPeriodDTO> GetTotalForPeriodAsync(DateTime startDate, DateTime endDate);
    }
}
