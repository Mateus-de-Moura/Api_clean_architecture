using api_clean_architecture.Application.UserCQ.Commands;
using api_clean_architecture.Application.UserCQ.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_clean_architecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IMediator madiator, IMapper mapper) : ControllerBase
    {
        private readonly IMediator _mediator = madiator;
        private readonly IMapper _mapper = mapper;

        [HttpPost("CreateUser")]
        public async Task<ActionResult<UserInfoViewModel>> CreateUser(CreateUserCommand command)
        {
            var request = await _mediator.Send(command);

            if (request.ResponseInfo is null)
            {
                var userInfo = request.Value;

                if (userInfo is not null)
                {
                    var cookiesOptionsToken = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        Expires = DateTimeOffset.UtcNow.AddDays(7),
                    };

                    var cookiesOptionsRefreshToken = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        Expires = DateTimeOffset.UtcNow.AddDays(7),
                    };

                    Response.Cookies.Append("jwt", request.Value!.TokenJwt!, cookiesOptionsToken);
                    Response.Cookies.Append("jwt", request.Value!.RefreshToken!, cookiesOptionsRefreshToken);

                    return Ok(_mapper.Map<UserInfoViewModel>(request.Value));
                }

            }

            return BadRequest(request);
        }
    }
}
