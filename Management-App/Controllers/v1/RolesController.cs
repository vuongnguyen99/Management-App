using Management.Data.Entities;
using Management_Common.Exception;
using Management_Core.Interface;
using Management_Core.Models.Role;
using Microsoft.AspNetCore.Mvc;

namespace Management_App.Controllers.v1
{
    [Route("api/v1/roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleServices _roleServices;
        public RolesController(IRoleServices roleServices)
        {
            _roleServices = roleServices;
        }
        /// <summary>
        /// Create new role. For admins only.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateRoleResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<IActionResult> CreateUser([FromBody] CreateRoleRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _roleServices.CreateRole(request, cancellationToken);
                return Ok(result);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
