using AutoMapper;
using JWTAuth.Core;
using JWTAuth.Core.Utilities.Security.Hashing;
using JWTAuth.Data;
using JWTAuth.Entities;
using JWTAuth.Entities.Dto;

namespace JWTAuth.Business
{
    public class AuthService : IAuthService
    {
        private readonly IApplicationUserService _applicationUserService;
        private readonly ITokenHelper _tokenHelper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AuthService(IApplicationUserService applicationUserService, ITokenHelper tokenHelper, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _applicationUserService = applicationUserService;
            _tokenHelper = tokenHelper;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
        public async Task<IDataResult<ApplicationUserReadDto>> GetUserInfo(int userId)
        {
            var user = await _applicationUserService.GetById(userId);
            var mappingUser = _mapper.Map<ApplicationUserReadDto>(user.Data);
            if (user.Success)
            {
                return new SuccessDataResult<ApplicationUserReadDto>(mappingUser, "Kullanıcı Bilgisi Başarıyla Getirildi");
            }
            return new ErrorDataResult<ApplicationUserReadDto>("Kullanıcı Bulunamadı....");


        }
        public async Task<IDataResult<ApplicationUser>> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = await _applicationUserService.GetByMail(userForLoginDto.Email);
            if (!userToCheck.Success)
            {
                return new ErrorDataResult<ApplicationUser>(userToCheck.Data, "Kullanıcı Bulunamadı");
            }

            if (!HashingHelper.VerifyPassowrdHash(userForLoginDto.Password, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt))
            {
                return new ErrorDataResult<ApplicationUser>(userToCheck.Data, "Şifre Hatalı");
            }

            return new SuccessDataResult<ApplicationUser>(userToCheck.Data, "Giriş Başarılı. Yönlendiriliyorsunuz...");
        }
        public async Task<IDataResult<ApplicationUser>> Register(UserForRegisterDto userForRegisterDto)
        {
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new ApplicationUser
            {
                UserName = userForRegisterDto.UserName,
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };
            await _applicationUserService.AddAsync(user);
            return new SuccessDataResult<ApplicationUser>(user, "Kullanıcı Kayıt Oldu");
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
    }
}
