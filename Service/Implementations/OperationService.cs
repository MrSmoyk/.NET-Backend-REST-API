using AutoMapper;
using DAL.Interfases;
using Domain.DTO;
using Domain.DTO.OperationDTOs;
using Domain.Entitys;
using Domain.Exceptions;
using Domain.Resourses;
using Service.Interfaces;

namespace Service.Implementations
{
    public class OperationService : BaseService<Operation, OperationDTO>, IOperationService
    {
        private IOperationRepository _operationRepository;
        private IOperationTypeRepository _operationTypeRepository;

        public OperationService(IOperationRepository operationRepository, IOperationTypeRepository operationTypeRepository, IMapper mapper)
            : base(operationRepository, mapper)
        {
            _operationRepository = operationRepository;
            _operationTypeRepository = operationTypeRepository;
        }

        public async Task<OperationDTO> CreateOperationAsync(OperationCreateUpdateDTO operationDTO)
        {
            if (operationDTO is null)
                throw new SourceEntityNullException(ErrorMessageResources.SourceEntityNullError);

            var operationType = await GetOperationTypeAsync(operationDTO.TypeName);
            var operation = mapper.Map<Operation>(operationDTO);
            operation.TypeId = operationType.Id;
            operation.Type = null;

            var createdOperation = await _operationRepository.CreateAsync(operation);

            return mapper.Map<OperationDTO>(createdOperation);
        }

        public async Task<OperationDTO> UpdateOperationAsync(Guid id, OperationCreateUpdateDTO operationDTO)
        {
            if (operationDTO is null)
                throw new SourceEntityNullException(ErrorMessageResources.SourceEntityNullError);

            var operationType = await GetOperationTypeAsync(operationDTO.TypeName);
            var operationToUpdate = mapper.Map<Operation>(operationDTO);

            operationToUpdate.Id = id;
            operationToUpdate.TypeId = operationType.Id;
            operationToUpdate.Type = null;


            var operation = await _operationRepository.UpdateAsync(operationToUpdate);
            return mapper.Map<OperationDTO>(operation);
        }

        public async Task<TotalForPeriodDTO> GetTotalForDateAsync(DateTime date)
        {
            var totalOperationsForPeriod = await _operationRepository.GetForDateAsync(date);
            return CreateTotal(totalOperationsForPeriod, date, date);
        }

        public async Task<TotalForPeriodDTO> GetTotalForPeriodAsync(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                throw new SourseDateException(string.Format(ErrorMessageResources.DateError,startDate,endDate));
            }
            var totalOperationsForPeriod = await _operationRepository.GetForPeriodAsync(startDate, endDate);
            return CreateTotal(totalOperationsForPeriod, startDate, endDate);
        }

        private async Task<OperationType> GetOperationTypeAsync(string typeName)
        {
            var operationType = await _operationTypeRepository.GetTypeByNameAsync(typeName);

            if (operationType is null)
            {
                throw new UnknownOperationTypeException(string.Format(ErrorMessageResources.TypeNameError,typeName));
            }

            return operationType;
        }



        private TotalForPeriodDTO CreateTotal(IEnumerable<Operation> operations, DateTime startDate, DateTime endDate)
        {
            var operationDTOs = mapper.Map<IEnumerable<OperationDTO>>(operations);
            int totalIncome = 0, totalExpenses = 0;

            foreach (var operation in operations)
            {
                if (operation.Type.IsIncome)
                {
                    totalIncome += operation.Amount;
                }
                else
                {
                    totalExpenses += operation.Amount;
                }
            }

            return new TotalForPeriodDTO
            {
                StartDate = startDate,
                EndDate = endDate,
                Operations = new List<OperationDTO>(operationDTOs),
                PeriodExpenses = totalExpenses,
                PeriodIncome = totalIncome,
                PeriodBalance = totalIncome - totalExpenses
            };
        }
    }
}
