﻿using api_clean_architecture.Application.Response;
using api_clean_architecture.Application.UserCQ.Commands;
using api_clean_architecture.Application.UserCQ.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace api_clean_architecture.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController(IMediator madiator, IMapper mapper) : ControllerBase
    {
        private readonly IMediator _mediator = madiator;
        private readonly IMapper _mapper = mapper;      

        [HttpPost("Login")]
        public async Task<ActionResult<ResponseBase<UserInfoViewModel>>> Login(LoginUserCommand command)
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
                    Response.Cookies.Append("refreshToken", request.Value!.RefreshToken!, cookiesOptionsRefreshToken);

                    return Ok(_mapper.Map<UserInfoViewModel>(request.Value));
                }

            }

            return BadRequest(request);
        }

        [HttpPost("RefreshToken")]
        public async Task<ActionResult<ResponseBase<UserInfoViewModel>>> RefreshToken(RefreshTokenCommand comand)
        {
            var request = await _mediator.Send(new RefreshTokenCommand
            {
                Username = comand.Username,
                RefreshToken = Request.Cookies["refreshToken"]
            });

            return Ok(request);
        }
    }
}
