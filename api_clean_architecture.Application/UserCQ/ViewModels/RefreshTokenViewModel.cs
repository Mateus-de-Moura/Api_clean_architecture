using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_clean_architecture.Application.UserCQ.ViewModels
{
    public record RefreshTokenViewModel
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? RefreshToken { get; set; }
        public string? TokenJwt { get; set; }
        public DateTime? RefreshTokenExpirationTime { get; set; }
    }
}
