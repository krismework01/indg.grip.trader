using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using INDG.GRIP.Trader.Application.Logic.Auth;

namespace INDG.GRIP.Trader.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/auth")]
    public class AuthController : BaseController
    {
        [HttpPost("signin")]
        public async Task<IActionResult> Login([FromBody] SignInCommand command, CancellationToken token)
        {
            var result = await Mediator.Send(command, token);
            if (result is null)
                return Unauthorized();
            
            return Ok(result);
        }
    }
}