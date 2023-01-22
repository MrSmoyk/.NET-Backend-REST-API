using AutoMapper;
using DAL.Interfases;
using Domain.DTO.TypeDTOs;
using Domain.Entitys;
using Domain.Exceptions;
using Domain.Resourses;
using Service.Interfaces;

namespace Service.Implementations
{
    public class OperationTypeService : BaseService<OperationType, OperationTypeDTO>, IOperationTypeService
    {
        private IOperationTypeRepository _operationTypeRepository;

        public OperationTypeService(IOperationTypeRepository operationTypeRepository, IMapper mapper)
            : base(operationTypeRepository, mapper)
        {
            _operationTypeRepository = operationTypeRepository;
        }

        public async Task<OperationTypeDTO> CreateOperationTypeAsync(OperationTypeCreateUpdateDTO operationTypeDTO)
        {
            if (operationTypeDTO is null)
                throw new SourceEntityNullException(ErrorMessageResources.SourceEntityNullError);

            if (operationTypeDTO.Name is null)
                throw new SourceEntityNullException(ErrorMessageResources.SourceEntityNullError);

            await CheckForUniqueName(operationTypeDTO.Name);

            var operationType = mapper.Map<OperationType>(operationTypeDTO);
            var createdOperationType = await _operationTypeRepository.CreateAsync(operationType);
            return mapper.Map<OperationTypeDTO>(createdOperationType);
        }

        public async Task<OperationTypeDTO> UpdateOperationTypeAsync(Guid id, OperationTypeCreateUpdateDTO operationTypeDTO)
        {
            if (!await _operationTypeRepository.ExistsAsync(id))
                throw new UnknownOperationTypeException(string.Format(ErrorMessageResources.TypeIdError, id));

            await CheckForUniqueName(operationTypeDTO.Name);

            var operationType = mapper.Map<OperationType>(operationTypeDTO);
            operationType.Id = id;

            var updatedOperationType = await _operationTypeRepository.UpdateAsync(operationType);
            return mapper.Map<OperationTypeDTO>(updatedOperationType);
        }

        private async Task CheckForUniqueName(string operationTypeName)
        {
            var operationType = await _operationTypeRepository.GetTypeByNameAsync(operationTypeName);
            if (operationType is not null)
            {
                throw new AddingExistingEntityException(string.Format(ErrorMessageResources.TypeExistError, operationTypeName));
            }
        }
    }
}
