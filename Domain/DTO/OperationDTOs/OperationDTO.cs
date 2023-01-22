using System.ComponentModel.DataAnnotations;

namespace Domain.DTO.OperationDTOs
{
    public class OperationDTO : BaseDTO
    {
        [Required]
        public string Type { get; set; }

        [Required]
        public Guid TypeId { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Amount must be greater than zero")]
        public int Amount { get; set; }

    }
}
