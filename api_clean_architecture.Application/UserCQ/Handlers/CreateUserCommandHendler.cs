using api_clean_architecture.Application.Response;
using api_clean_architecture.Application.UserCQ.Commands;
using api_clean_architecture.Application.UserCQ.ViewModels;
using api_clean_architecture.Domain.Entity;
using api_clean_architecture.Infra.Data.Data;
using AutoMapper;
using MediatR;

namespace api_clean_architecture.Application.UserCQ.Handlers
{
    public class CreateUserCommandHendler(TasksDbContext context, IMapper mapper) : IRequestHandler<CreateUserCommand, ResponseBase<UserInfoViewModel?>>
    {
        private readonly TasksDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<ResponseBase<UserInfoViewModel>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);           

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return new ResponseBase<UserInfoViewModel>
            {
                ResponseInfo = null,
                Value = _mapper.Map<UserInfoViewModel>(user)
            };
        }
    }
}
