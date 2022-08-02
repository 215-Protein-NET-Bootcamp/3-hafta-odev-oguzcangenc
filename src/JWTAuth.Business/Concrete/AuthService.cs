using AutoMapper;
using JWTAuth.Business.Constant;
using JWTAuth.Core;
using JWTAuth.Core.Utilities.Security.Hashing;
using JWTAuth.Data;
using JWTAuth.Entities;
using JWTAuth.Entities.Dto;
using Microsoft.AspNetCore.Http;

namespace JWTAuth.Business
{
    public class AuthService : IAuthService
    {
        private readonly IApplicationUserService _applicationUserService;
        private readonly ITokenHelper _tokenHelper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IApplicationUserService applicationUserService, ITokenHelper tokenHelper, IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _applicationUserService = applicationUserService;
            _tokenHelper = tokenHelper;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;

        }
        public async Task<IResult> ChangePassword(string oldPassword, string newPassword, string confirmNewPassword, int userId)
        {
            var user = (await _applicationUserService.GetById(userId)).Data;
            if (!HashingHelper.VerifyPassowrdHash(oldPassword, user.PasswordHash, user.PasswordSalt))
            {
                return new ErrorResult(Messages.OLD_PASSWORD_INCORRECT);

            }
            HashingHelper.CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.ModifiedDate = DateTime.UtcNow;
            var result = _applicationUserService.Update(user);
            await _unitOfWork.CompleteAsync();
            var response = _mapper.Map<ApplicationUserReadDto>(user);
            if (result.Success)
            {
                return new SuccessDataResult<ApplicationUserReadDto>(response,Messages.CHANGE_PASSWORD_SUCCESS);

            }
            return new ErrorResult(Messages.CHANGE_PASSWORD_ERROR);

        }
        public IDataResult<AccessToken> CreateAccessToken(ApplicationUser user)
        {
            var accessToken = _tokenHelper.CreateToken(user);
            return new SuccessDataResult<AccessToken>(accessToken,Messages.TOKEN_GENERATE);
        }
        public async Task<IDataResult<ApplicationUserReadDto>> GetUserInfo()
        {
            var user = await _applicationUserService.GetById(CurrentUserId);
            var mappingUser = _mapper.Map<ApplicationUserReadDto>(user.Data);
            if (user.Success)
            {
                return new SuccessDataResult<ApplicationUserReadDto>(mappingUser,Messages.GET_APPLICATION_USER);
            }
            return new ErrorDataResult<ApplicationUserReadDto>(Messages.USER_NOTFOUND);


        }
        public async Task<IDataResult<AccessToken>> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = await _applicationUserService.GetByMail(userForLoginDto.Email);
            if (!userToCheck.Success)
            {
                return new ErrorDataResult<AccessToken>(Messages.USER_NOTFOUND);
            }

            if (!HashingHelper.VerifyPassowrdHash(userForLoginDto.Password, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt))
            {
                return new ErrorDataResult<AccessToken>(Messages.PASSWORD_INCORRECT);
            }
            var resultToken = CreateAccessToken(userToCheck.Data);
            if (resultToken.Success)
            {
                return new SuccessDataResult<AccessToken>(resultToken.Data,Messages.LOGIN_SUCCESS);
            }
            return new ErrorDataResult<AccessToken>(Messages.SYSTEM_ERROR);

        }
        public async Task<IDataResult<ApplicationUserReadDto>> Register(UserForRegisterDto userForRegisterDto)
        {
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new ApplicationUser
            {
                UserName = userForRegisterDto.UserName,
                Email = userForRegisterDto.Email,
                Name = userForRegisterDto.Name,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                ModifiedDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                LastActivity = DateTime.UtcNow
            };
            await _applicationUserService.AddAsync(user);
            return new SuccessDataResult<ApplicationUserReadDto>(_mapper.Map<ApplicationUserReadDto>(user),Messages.REGISTER_USER);
        }
        public async Task<IResult> UserExists(string mail, string username)
        {
            var result = await _applicationUserService.GetByMailAndUsername(mail, username);
            if (result.Success)
            {
                return new ErrorResult(Messages.USER_ALREADY_EXISTS);
            }
            return new SuccessResult();
        }
        public async Task<IDataResult<ApplicationUserReadDto>> EditUser(UserForEditDto userForEditDto)
        {
            ApplicationUser user = (await _applicationUserService.GetById(CurrentUserId)).Data;
            if (user != null)
            {
                user.Name = userForEditDto.Name;
                user.ModifiedDate = DateTime.UtcNow;
                user.LastActivity = DateTime.UtcNow;
                _applicationUserService.Update(user);
                await _unitOfWork.CompleteAsync();
                return new SuccessDataResult<ApplicationUserReadDto>(_mapper.Map<ApplicationUserReadDto>(user));
            }
            return new ErrorDataResult<ApplicationUserReadDto>();

        }

        public async Task<IResult> DeleteAuth()
        {
            var user = await _applicationUserService.GetById(CurrentUserId);
            var response =await _applicationUserService.Delete(user.Data);
            if (response.Success)
            {
                return new SuccessResult(Messages.SUCCESS_TRANSACTION);
            }
            return new ErrorResult(Messages.SYSTEM_ERROR);
        }

        public int CurrentUserId
        {
            get
            {
                if (_httpContextAccessor.HttpContext != null)
                {
                    var claimValue = _httpContextAccessor.HttpContext?.User?.FindFirst(t => t.Type == "AccountId");
                    if (claimValue != null)
                    {
                        return Convert.ToInt32(claimValue.Value);
                    }
                    return 0;
                }
                else
                {
                    return 0;
                }
            }
            set => throw new NotImplementedException();
        }
    }
}
