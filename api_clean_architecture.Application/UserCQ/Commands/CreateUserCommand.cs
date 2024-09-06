﻿using api_clean_architecture.Application.Response;
using api_clean_architecture.Application.UserCQ.ViewModels;
using MediatR;

namespace api_clean_architecture.Application.UserCQ.Commands
{
    public record CreateUserCommand : IRequest<ResponseBase<UserInfoViewModel?>>
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Username { get; set; }
    }
}
