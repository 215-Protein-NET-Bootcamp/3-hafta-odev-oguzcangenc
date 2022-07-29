﻿

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

        public IResult Delete(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<ApplicationUser>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IDataResult<ApplicationUser> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<ApplicationUser>> GetByMail(string mail)
        {
            var response = await _applicationUserRepository.Get(user => user.Email == mail);
            if (response == null)
            {
                return new ErrorDataResult<ApplicationUser>();
            }
            return new SuccessDataResult<ApplicationUser>(response);
        }

        public async Task<IDataResult<ApplicationUser>> GetByMailAndUsername(string mail, string username)
        {
            var response = await _applicationUserRepository.Get(user => user.Email == mail || user.UserName == username);
            if (response == null)
            {
                return new ErrorDataResult<ApplicationUser>();
            }
            return new SuccessDataResult<ApplicationUser>();
        }

        public IResult SetUserUpdate(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public IResult Update(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
    }
}