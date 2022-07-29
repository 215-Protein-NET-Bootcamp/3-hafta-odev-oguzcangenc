using JWTAuth.Business;
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
        public async Task<ActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = await _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            { 
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            var userExists = await _authService.UserExists(userForRegisterDto.Email,userForRegisterDto.UserName);
            if (!userExists.Success)
            {
                return BadRequest(userExists);
            }
            var registerResult =await  _authService.Register(userForRegisterDto);
            if (registerResult.Success)
            {
                return Ok(registerResult);
            }
            return BadRequest(registerResult);
        }
        [Authorize]
        [HttpPost("change-password")]
        public async Task<ActionResult> ChangePassword(UserForChangePassword userForChangePassword)
        {
            int userId = int.Parse(User.Claims.Where(x => x.Type == "AccountId").FirstOrDefault().Value);
            var userExists = await _authService.ChangePassword(userForChangePassword.OldPassword, userForChangePassword.NewPassword, userForChangePassword.ConfirmNewPassword, userId);
            if (!userExists.Success)
            {
                return BadRequest(userExists);
            }
           
            return BadRequest();
        }

    }
}
