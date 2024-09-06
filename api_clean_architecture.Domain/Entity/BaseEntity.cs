using System.ComponentModel.DataAnnotations;

namespace api_clean_architecture.Domain.Entity
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
