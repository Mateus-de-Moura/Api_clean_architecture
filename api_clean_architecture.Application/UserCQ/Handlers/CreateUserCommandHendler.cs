using api_clean_architecture.Application.Response;
using api_clean_architecture.Application.UserCQ.Commands;
using api_clean_architecture.Application.UserCQ.ViewModels;
using api_clean_architecture.Domain.Abstractions;
using api_clean_architecture.Domain.Entity;
using api_clean_architecture.Infra.Data.Data;
using AutoMapper;
using MediatR;

namespace api_clean_architecture.Application.UserCQ.Handlers
{
    public class CreateUserCommandHendler(TasksDbContext context, IMapper mapper,
        IAuthService authService ) : IRequestHandler<CreateUserCommand, ResponseBase<UserInfoViewModel?>>
    {
        private readonly TasksDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly IAuthService _authService = authService;

        public async Task<ResponseBase<UserInfoViewModel>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var passwordSalt = BCrypt.Net.BCrypt.GenerateSalt();
            var passWordHash = BCrypt.Net.BCrypt.HashPassword(request.Password, passwordSalt);

            var user = _mapper.Map<User>(request);   
            user.RefreshToken = _authService.GenerateRefreshToken();
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passWordHash;          

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var userInfoVM = _mapper.Map<UserInfoViewModel>(user);
            userInfoVM.TokenJwt = _authService.GenerateJWT(user.Email!, user.UserName!);         

            return new ResponseBase<UserInfoViewModel>
            {
                ResponseInfo = null,
                Value = userInfoVM,
            };
        }
    }
}
