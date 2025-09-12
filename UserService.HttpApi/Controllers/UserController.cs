using Common.HttpApi.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using UseCases.Common.Interfaces;
using UserService.UseCases.Commands;
using UserService.UseCases.Queries;

namespace UserService.HttpApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetUserQuery request)
        {
            var result = await Sender.Send(request);

            return ReturnResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserCommand request)
        {
            var result = await Sender.Send(request);

            return ReturnResult(result);
        }

        [HttpGet("test")]
        [Authorize]
        public IActionResult Get([FromServices] IUser user)
        {
            return Ok(user);
        }
    }
}
