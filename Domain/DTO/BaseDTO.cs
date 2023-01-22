using System.ComponentModel.DataAnnotations;

namespace Domain.DTO
{
    public abstract class BaseDTO
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
