using Management.Common.Exception;
using Management.Core.Models.Authenticate;
using Management.Core.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Management.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateService _authenticationService;
        public AuthenticateController(IAuthenticateService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginSystem([FromBody] Authenticate request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _authenticationService.LoginSystem(request, cancellationToken);
                return Ok(result);
            }
            catch(Exception ex)
            {
                switch(ex)
                {
                    case NotFoundException: return NotFound(ex.Message);
                    case ValidationException: return BadRequest(ex.Message);
                        default:  return StatusCode(500, ex.Message );
                }
            }
        }
    }
}
