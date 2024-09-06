using api_clean_architecture.Domain.Enum;

namespace api_clean_architecture.Domain.Entity
{
    public class Workspace : BaseEntity
    {
        public string? Title { get; set; }
        public User? User { get; set; }
        public ICollection<ListCard>? ListCards { get; set; }
        public StatusItemEnum Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
