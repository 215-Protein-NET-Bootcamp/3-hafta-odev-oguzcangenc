using AutoMapper;
using JWTAuth.Core;
using JWTAuth.Data;
using JWTAuth.Entities;
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
        public EmployeeService(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _employeeRepository = employeeRepository;
        }
        public async Task<IResult> AddAsync(EmployeeAddDto employee, int userId)
        {
            var mappingResult = _mapper.Map<Employee>(employee);
            mappingResult.ApplicationUserId = userId;
            await _employeeRepository.AddAsync(mappingResult);
            await _unitOfWork.CompleteAsync();
            return new SuccessDataResult<EmployeeAddDto>(employee, "Çalışan Kayıt Oldu...");
        }
        public IResult Delete(int employeeId)
        {
            throw new NotImplementedException();
        }
        public async Task<IDataResult<ICollection<EmployeeReadDto>>> GetAllAsync()
        {
            var response = await _employeeRepository.GetAllAsync();
            return new SuccessDataResult<ICollection<EmployeeReadDto>>(_mapper.Map<ICollection<EmployeeReadDto>>(response));
        }
        public Task<IDataResult<EmployeeReadDto>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        public IResult Update(Employee user)
        {
            throw new NotImplementedException();
        }
    }
}
