using AutoMapper;
using JWTAuth.Entities;
using JWTAuth.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuth.Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegisterDto, ApplicationUser>().ReverseMap();
            CreateMap<ApplicationUserReadDto, ApplicationUser>().ReverseMap();
            CreateMap<EmployeeAddDto, Employee>().ReverseMap();
            CreateMap<EmployeeReadDto, Employee>().ReverseMap();
            CreateMap<EmployeeUpdateDto, Employee>().ReverseMap();
            CreateMap<UserForEditDto, ApplicationUser>().ReverseMap();


        }
    }
}
