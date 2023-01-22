using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entitys
{
    public class Operation : BaseEntity
    {
        public DateTime Created { get; set; }

        public int Amount { get; set; }

        [Required]
        public Guid TypeId { get; set; }

        [ForeignKey("TypeId")]
        public virtual OperationType Type { get; set; }
    }
}
