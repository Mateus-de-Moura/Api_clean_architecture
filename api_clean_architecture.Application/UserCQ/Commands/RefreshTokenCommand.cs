using api_clean_architecture.Application.Response;
using api_clean_architecture.Application.UserCQ.ViewModels;
using MediatR;

namespace api_clean_architecture.Application.UserCQ.Commands
{
    public record RefreshTokenCommand : IRequest<ResponseBase<RefreshTokenViewModel>>
    {
        public string? Username { get; set; }
        public string? RefreshToken { get; set; }
    }
}
