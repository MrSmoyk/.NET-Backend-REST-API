using Domain.DTO.TypeDTOs;
using Domain.Entitys;

namespace Service.Interfaces
{
    public interface IOperationTypeService : IBaseService<OperationType, OperationTypeDTO>
    {
        public Task<OperationTypeDTO> CreateOperationTypeAsync(OperationTypeCreateUpdateDTO operationTypeDTO);
        public Task<OperationTypeDTO> UpdateOperationTypeAsync(Guid id, OperationTypeCreateUpdateDTO operationTypeDTO);
    }
}
