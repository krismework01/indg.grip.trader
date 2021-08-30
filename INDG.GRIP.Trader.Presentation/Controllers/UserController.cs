using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using INDG.GRIP.Trader.Application.Common.Models;
using INDG.GRIP.Trader.Application.Logic.Users.Create;

namespace INDG.GRIP.Trader.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/users")]
    public class UserController : BaseController
    {
        [HttpPost]
        public Task<Result<Guid>> AddUsers(CreateUserCommand command, CancellationToken token)
        {
            return Mediator.Send(command, token);
        }
    }
}