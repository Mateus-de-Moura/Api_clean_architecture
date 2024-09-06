using api_clean_architecture.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_clean_architecture.Domain.Abstractions
{
    public interface IAuthService
    {
        public String GenerateJWT(string email, string username);
        public String GenerateRefreshToken();
        Task<ValidationFieldsUserEnum> UniqueEmailAndUserName(string email, string username);
    }
}
