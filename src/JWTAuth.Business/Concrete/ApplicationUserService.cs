using AutoMapper;
using JWTAuth.Core;
using JWTAuth.Data;
using JWTAuth.Entities;

namespace JWTAuth.Business
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ApplicationUserService(IApplicationUserRepository applicationUserRepository, IUnitOfWork unitOfWork)
        {
            _applicationUserRepository = applicationUserRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<IResult> AddAsync(ApplicationUser user)
        {
            await _applicationUserRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();
            return new SuccessResult();

        }
        public async Task<IResult> Delete(ApplicationUser user)
        {
            _applicationUserRepository.Delete(user);
            await _unitOfWork.CompleteAsync();

            return new SuccessResult("App User Silindi...");
        }
        public async Task<IDataResult<ApplicationUser>> GetById(int id)
        {
            var response = await _applicationUserRepository.GetAsync(user => user.Id == id);
            if (response == null)
            {
                return new ErrorDataResult<ApplicationUser>();
            }
            return new SuccessDataResult<ApplicationUser>(response);
        }
        public async Task<IDataResult<ApplicationUser>> GetByMail(string mail)
        {
            var response = await _applicationUserRepository.GetAsync(user => user.Email == mail);
            if (response == null)
            {
                return new ErrorDataResult<ApplicationUser>();
            }
            return new SuccessDataResult<ApplicationUser>(response);
        }
        public async Task<IDataResult<ApplicationUser>> GetByMailAndUsername(string mail, string username)
        {
            var response = await _applicationUserRepository.GetAsync(user => user.Email == mail || user.UserName == username);
            if (response == null)
            {
                return new ErrorDataResult<ApplicationUser>();
            }
            return new SuccessDataResult<ApplicationUser>();
        }
        public IResult Update(ApplicationUser user)
        {
            _applicationUserRepository.Update(user);
            return new SuccessDataResult<ApplicationUser>(user);
        }
    }
}
