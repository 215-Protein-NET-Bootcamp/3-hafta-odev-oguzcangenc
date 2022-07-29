using JWTAuth.Core;
using JWTAuth.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuth.Business
{
    public interface IEmployeeService
    {
        Task<IDataResult<ICollection<EmployeeReadDto>>> GetAllAsync();
        Task<IDataResult<EmployeeReadDto>> GetByIdAsync(int id);
        Task<IResult> AddAsync(EmployeeAddDto employee);
        Task<IResult> UpdateAsync(EmployeeUpdateDto employee);
        Task<IResult> DeleteAsync(EmployeeReadDto employee);


    }
}
