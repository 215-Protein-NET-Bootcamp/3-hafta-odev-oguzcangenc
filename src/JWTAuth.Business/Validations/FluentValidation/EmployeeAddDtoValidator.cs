using FluentValidation;
using JWTAuth.Entities;

namespace JWTAuth.Business.Validations.FluentValidation
{
    public class EmployeeAddDtoValidator : AbstractValidator<EmployeeAddDto>
    {
        public EmployeeAddDtoValidator()
        {
            RuleFor(request => request.DateOfBirth).NotEmpty();
            RuleFor(request => request.FirstName).NotEmpty();
            RuleFor(request => request.LastName).NotEmpty();
            RuleFor(request => request.Description).NotEmpty();
            RuleFor(request => request.Phone).NotEmpty();
        }
    }
}
