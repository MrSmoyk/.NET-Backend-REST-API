using Domain.DTO.OperationDTOs;

namespace Domain.DTO
{
    public class TotalForPeriodDTO
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int PeriodIncome { get; set; }

        public int PeriodExpenses { get; set; }

        public int PeriodBalance { get; set; }

        public List<OperationDTO> Operations { get; set; }
    }
}
