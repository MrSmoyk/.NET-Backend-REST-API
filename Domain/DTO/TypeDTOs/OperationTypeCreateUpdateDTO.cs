using System.ComponentModel.DataAnnotations;

namespace Domain.DTO.TypeDTOs
{
    public class OperationTypeCreateUpdateDTO
    {
        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public bool IsIncome { get; set; }
    }
}
