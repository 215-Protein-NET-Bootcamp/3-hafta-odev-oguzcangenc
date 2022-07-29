using AutoMapper;
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
                return new ErrorResult("Eski Şifre Hatalı");

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
                return new SuccessDataResult<ApplicationUserReadDto>(response, "Şifre Başarıyla Değiştirildi...");

            }
            return new ErrorResult("Şifre Değiştirme Hatası..");

        }
        public IDataResult<AccessToken> CreateAccessToken(ApplicationUser user)
        {
            var accessToken = _tokenHelper.CreateToken(user);
            return new SuccessDataResult<AccessToken>(accessToken, "Token Oluşturuldu");
        }
        public async Task<IDataResult<ApplicationUserReadDto>> GetUserInfo()
        {
            var user = await _applicationUserService.GetById(CurrentUserId);
            var mappingUser = _mapper.Map<ApplicationUserReadDto>(user.Data);
            if (user.Success)
            {
                return new SuccessDataResult<ApplicationUserReadDto>(mappingUser, "Kullanıcı Bilgisi Başarıyla Getirildi");
            }
            return new ErrorDataResult<ApplicationUserReadDto>("Kullanıcı Bulunamadı....");


        }
        public async Task<IDataResult<AccessToken>> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = await _applicationUserService.GetByMail(userForLoginDto.Email);
            if (!userToCheck.Success)
            {
                return new ErrorDataResult<AccessToken>("Kullanıcı Bulunamadı");
            }

            if (!HashingHelper.VerifyPassowrdHash(userForLoginDto.Password, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt))
            {
                return new ErrorDataResult<AccessToken>("Şifre Hatalı");
            }
            var resultToken = CreateAccessToken(userToCheck.Data);
            if (resultToken.Success)
            {
                return new SuccessDataResult<AccessToken>(resultToken.Data, "Giriş Başarılı...");
            }
            return new ErrorDataResult<AccessToken>("Sistem Hatası...");

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
            return new SuccessDataResult<ApplicationUserReadDto>(_mapper.Map<ApplicationUserReadDto>(user), "Kullanıcı Kayıt Oldu");
        }
        public async Task<IResult> UserExists(string mail, string username)
        {
            var result = await _applicationUserService.GetByMailAndUsername(mail, username);
            if (result.Success)
            {
                return new ErrorResult("Kullanıcı Zaten Mevcut");
            }
            return new SuccessResult();
        }

        public async Task<IDataResult<ApplicationUserReadDto>> EditUser(UserForEditDto userForEditDto)
        {
            ApplicationUser user = (await _applicationUserService.GetById(CurrentUserId)).Data;
            if (user != null)
            {
                var tempData = _mapper.Map<ApplicationUser>(userForEditDto);
                user = tempData;
                await _unitOfWork.CompleteAsync();
                return new SuccessDataResult<ApplicationUserReadDto>(_mapper.Map<ApplicationUserReadDto>(user));
            }
            return new ErrorDataResult<ApplicationUserReadDto>();

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
