using api_clean_architecture.Application.Response;
using api_clean_architecture.Application.UserCQ.Commands;
using api_clean_architecture.Application.UserCQ.ViewModels;
using api_clean_architecture.Domain.Abstractions;
using api_clean_architecture.Infra.Data.Data;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace api_clean_architecture.Application.UserCQ.Handlers
{
    public class RefreshTokenCommandHandler(TasksDbContext context, IMapper mapper,
        IAuthService authService) : IRequestHandler<RefreshTokenCommand, ResponseBase<RefreshTokenViewModel>>
    {
        private readonly TasksDbContext _context =  context;
        private readonly IMapper _mapper = mapper;
        private readonly IAuthService _authService = authService;
        public async Task<ResponseBase<RefreshTokenViewModel>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == request.Username);

            if (user == null || user.RefreshToken != request.RefreshToken 
                || user.RefreshTokenExpirationTime < DateTime.Now) 
            {
                return new ResponseBase<RefreshTokenViewModel>
                {
                    ResponseInfo = new()
                    {
                        Title = "Token inválido",
                        ErrorDescription = "RefreshToken inválido ou  expirado. Faça login novamente",
                        HttpStatus = 400
                    },
                    Value = null
                };

            }
           
            user.RefreshToken = _authService.GenerateRefreshToken();
            user.RefreshTokenExpirationTime = DateTime.Now.AddDays(7);

            await _context.SaveChangesAsync();

            RefreshTokenViewModel refreshTokenVM = _mapper.Map<RefreshTokenViewModel>(user);
            refreshTokenVM.RefreshToken = _authService.GenerateJWT(user.Email!, user.UserName!);

            return new ResponseBase<RefreshTokenViewModel>
            {
                ResponseInfo = null,
                Value = refreshTokenVM
            };
        }
    }
}
