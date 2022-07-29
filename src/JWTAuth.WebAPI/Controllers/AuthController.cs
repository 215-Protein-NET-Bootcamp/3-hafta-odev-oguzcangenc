using JWTAuth.Business;
using JWTAuth.Core;
using JWTAuth.Entities;
using JWTAuth.Entities.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace JWTAuth.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = await _authService.Login(userForLoginDto);
            if (userToLogin.Success)
            {
                return Ok(userToLogin);
            }
            return BadRequest(userToLogin);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            var userExists = await _authService.UserExists(userForRegisterDto.Email, userForRegisterDto.UserName);
            if (!userExists.Success)
            {
                return BadRequest(userExists);
            }
            var registerResult = await _authService.Register(userForRegisterDto);
            if (registerResult.Success)
            {
                return Ok(registerResult);
            }
            return BadRequest(registerResult);
        }
        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(UserForChangePassword userForChangePassword)
        {
            int userId = int.Parse(User.Claims.First(x => x.Type == "AccountId").Value);
            var response = await _authService.ChangePassword(userForChangePassword.OldPassword, userForChangePassword.NewPassword, userForChangePassword.ConfirmNewPassword, userId);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        [HttpPut("edit-user")]
        public async Task<IActionResult> EditUser(UserForEditDto userForEditDto)
        {

            var response = await _authService.EditUser(userForEditDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        [Authorize]
        [HttpGet("get-current-user")]
        public async Task<IActionResult> GetUserInfo()
        {
            var response = await _authService.GetUserInfo();
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
