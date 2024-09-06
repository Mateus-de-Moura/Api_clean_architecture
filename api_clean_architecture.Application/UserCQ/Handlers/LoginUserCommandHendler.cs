using api_clean_architecture.Application.Response;
using api_clean_architecture.Application.UserCQ.Commands;
using api_clean_architecture.Application.UserCQ.ViewModels;
using api_clean_architecture.Domain.Abstractions;
using api_clean_architecture.Infra.Data.Data;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_clean_architecture.Application.UserCQ.Handlers
{
    public record LoginUserCommandHendler(TasksDbContext contex, IAuthService AuthService,
        IMapper Mapper) : IRequestHandler<LoginUserCommand, ResponseBase<RefreshTokenViewModel>>
    {
        private readonly TasksDbContext _contex = contex;
        private readonly IAuthService _authService = AuthService;
        private readonly IMapper _mapper = Mapper;
        public async Task<ResponseBase<RefreshTokenViewModel>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _contex.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user is null)
            {
                return new ResponseBase<RefreshTokenViewModel>()
                {
                    ResponseInfo = new()
                    {
                        Title = "Usuário não encontrado",
                        ErrorDescription = "Nenhum usuário encontrado com o email informado.",
                        HttpStatus = 404
                    },
                    Value = null,
                };
            }

            if (BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash) is true)
            {
                user.RefreshToken = _authService.GenerateRefreshToken();
                user.RefreshTokenExpirationTime = DateTime.Now.AddDays(7);

                _contex.Update(user);
                await _contex.SaveChangesAsync();

                RefreshTokenViewModel refreshTokenVM = _mapper.Map<RefreshTokenViewModel>(user);
                refreshTokenVM.TokenJwt = _authService.GenerateJWT(user.Email!, user.UserName!);

                return new ResponseBase<RefreshTokenViewModel>()
                {
                    ResponseInfo = null,
                    Value = refreshTokenVM
                };
            }
            else
            {
                return new ResponseBase<RefreshTokenViewModel>()
                {
                    ResponseInfo = new()
                    {
                        Title = "Senha incorreta",
                        ErrorDescription = "A senha informada está incorreta.",
                        HttpStatus = 404
                    },
                    Value = null,
                };
            }
        }
    }
}
