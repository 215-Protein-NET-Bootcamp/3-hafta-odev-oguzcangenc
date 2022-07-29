using AutoMapper;
using JWTAuth.Core;
using JWTAuth.Data;
using JWTAuth.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuth.Business
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmployeeService(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _employeeRepository = employeeRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IResult> AddAsync(EmployeeAddDto employee)
        {
            var mappingResult = _mapper.Map<Employee>(employee);
            mappingResult.ApplicationUserId = CurrentUserId;
            await _employeeRepository.AddAsync(mappingResult);
            await _unitOfWork.CompleteAsync();
            return new SuccessDataResult<EmployeeAddDto>(employee, "Çalışan Kayıt Oldu...");
        }
        public async Task<IResult> DeleteAsync(EmployeeReadDto employeeReadDto)
        {
            var deletedUser = _mapper.Map<Employee>(employeeReadDto);
            _employeeRepository.Delete(deletedUser);
            await _unitOfWork.CompleteAsync();
            return new SuccessResult("Çalışan Silindi");
        }
        public async Task<IDataResult<ICollection<EmployeeReadDto>>> GetAllAsync()
        {
            var response = await _employeeRepository.GetAllAsync();
            if (response.Count() > 0)
            {
                return new SuccessDataResult<ICollection<EmployeeReadDto>>(_mapper.Map<ICollection<EmployeeReadDto>>(response));

            }
            return new ErrorDataResult<ICollection<EmployeeReadDto>>("Kullanıcı Bulunamadı...");
        }
        public async Task<IDataResult<EmployeeReadDto>> GetByIdAsync(int id)
        {
            var response = await _employeeRepository.GetAsync(emp => emp.Id == id);
            if (response != null)
            {
                return new SuccessDataResult<EmployeeReadDto>(_mapper.Map<EmployeeReadDto>(response));

            }
            return new ErrorDataResult<EmployeeReadDto>("Çalışan Bulunamadı");
        }
        public async Task<IResult> UpdateAsync(EmployeeUpdateDto employee)
        {
            var tempData = _mapper.Map<Employee>(employee);
            tempData.ApplicationUserId = CurrentUserId;
            _employeeRepository.Update(tempData);
            await _unitOfWork.CompleteAsync();
            return new SuccessResult("Çalışan Güncellendi");
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
