using api_clean_architecture.Domain.Enum;

namespace api_clean_architecture.Domain.Entity
{
    public class Card : BaseEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? DeadLine { get; set; }
        public ListCard? List {  get; set; }
        public StatusCardEnum Status {  get; set; }
    }
}
