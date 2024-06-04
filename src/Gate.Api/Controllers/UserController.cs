using Microsoft.AspNetCore.Mvc;
using Gate.Application.DTOs.Request;
using Microsoft.AspNetCore.Authorization;
using Gate.Application.DTOs.Response;
using Gate.Identity.BusinessLogic.Interfaces;

namespace Gate.Api.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        public UserController(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        
        [AllowAnonymous]
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser(RegisterUserRequest userRequest)
        {
            try
            {
                var result = await _identityService.RegisterIdentityUser(userRequest);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<UserLoginResponse>> Login(UserLoginRequest userRequest)
        {
            try
            {
                var result = await _identityService.Login(userRequest);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}