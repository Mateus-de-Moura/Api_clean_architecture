using api_clean_architecture.Application.Response;
using api_clean_architecture.Application.UserCQ.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_clean_architecture.Application.UserCQ.Commands
{
    public record LoginUserCommand : IRequest<ResponseBase<RefreshTokenViewModel>> 
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
