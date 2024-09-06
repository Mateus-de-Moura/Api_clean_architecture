using api_clean_architecture.Domain.Enum;

namespace api_clean_architecture.Domain.Entity
{
    public class ListCard : BaseEntity
    {
        public string? Title { get; set; }
        public StatusItemEnum Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Workspace? Workspace { get; set; }    
        public ICollection<Card>? Cards { get; set; }
    }
}
