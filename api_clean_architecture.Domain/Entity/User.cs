using System.ComponentModel.DataAnnotations;

namespace api_clean_architecture.Domain.Entity
{
    public class User : BaseEntity
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string? Name { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string? Surname { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? PasswordHash { get; set; }

        [Required]
        public string? UserName { get; set; }

        public ICollection<Workspace>? Workspaces { get; set; } 
        public string? RefreshToken {  get; set; }
        public DateTime? RefreshTokenExpirationTime {  get; set; }
        

    }
}
