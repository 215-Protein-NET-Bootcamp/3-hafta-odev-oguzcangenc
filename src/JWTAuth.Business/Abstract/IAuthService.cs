using JWTAuth.Core;
using JWTAuth.Entities;
using JWTAuth.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuth.Business
{
    public interface IAuthService
    {
        Task<IDataResult<ApplicationUser>> Register(UserForRegisterDto userForRegisterDto);
        Task<IDataResult<ApplicationUser>> Login(UserForLoginDto userForLoginDto);
        Task<IResult> UserExists(string mail, string username);
        IDataResult<AccessToken> CreateAccessToken(ApplicationUser user);
        Task<IResult> ChangePassword(string oldPassword, string newPassword, string confirmNewPassword,int userId);
        Task<IDataResult<ApplicationUserReadDto>> GetUserInfo(int userId);
    }
}
