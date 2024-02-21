using Microsoft.AspNetCore.Mvc;
using Gate.Domain.Models;
using Gate.Application.Services.Interfaces;
using Gate.Application.DTOs.Request;
using Microsoft.AspNetCore.Authorization;

namespace Gate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IIdentityService _identityService;
        public UserController(IUserService userService, IIdentityService identityService)
        {
            _userService = userService;
            _identityService = identityService;
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = _userService.GetById(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetAll")]
        public ActionResult GetAll()
        {
            try
            {
                var result = _userService.GetAll();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
        [AllowAnonymous]
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser(RegisterUserRequest userRequest)
        {
            try
            {
                var result = await _userService.RegisterUser(userRequest);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login(UserLoginRequest userRequest)
        {
            try
            {
                var result = _identityService.Login(userRequest);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}