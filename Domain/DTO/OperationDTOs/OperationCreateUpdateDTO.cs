using System.ComponentModel.DataAnnotations;

namespace Domain.DTO.OperationDTOs
{
    public class OperationCreateUpdateDTO
    {
        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public string TypeName { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Amount must be greater than zero")]
        public int Amount { get; set; }
    }
}
