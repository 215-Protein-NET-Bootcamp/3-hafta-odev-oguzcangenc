using JWTAuth.Core;
using JWTAuth.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuth.Business
{
    public interface IApplicationUserService
    {
        Task<IResult> AddAsync(ApplicationUser user);
        IResult Update(ApplicationUser user);
        Task<IResult> Delete(ApplicationUser user);
        Task<IDataResult<ApplicationUser>> GetById(int id);
        Task<IDataResult<ApplicationUser>> GetByMail(string mail);
        Task<IDataResult<ApplicationUser>> GetByMailAndUsername(string mail, string username);
    }
}
