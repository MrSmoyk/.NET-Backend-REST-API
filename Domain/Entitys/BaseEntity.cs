using System.ComponentModel.DataAnnotations;

namespace Domain.Entitys
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
