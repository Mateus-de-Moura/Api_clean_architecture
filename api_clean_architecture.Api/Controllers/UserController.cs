using api_clean_architecture.Application.UserCQ.Commands;
using api_clean_architecture.Application.UserCQ.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace api_clean_architecture.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController(IMediator madiator) : ControllerBase
    {
        private readonly IMediator _mediator = madiator;

        [HttpPost("CreateUser")]
        public async Task<ActionResult<UserInfoViewModel>> CreateUser(CreateUserCommand command)
        {          
            return Ok(await _mediator.Send(command));
        }
    }
}
