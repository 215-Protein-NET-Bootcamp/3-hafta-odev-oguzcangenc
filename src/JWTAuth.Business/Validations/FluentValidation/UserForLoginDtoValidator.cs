using FluentValidation;
using JWTAuth.Entities.Dto;

namespace JWTAuth.Business.Validations.FluentValidation
{
    public class UserForLoginDtoValidator : AbstractValidator<UserForLoginDto>
    {
        public UserForLoginDtoValidator()
        {
            RuleFor(c => c.Email).EmailAddress().NotEmpty();
            RuleFor(c => c.Password).NotEmpty();
        }
    }
}
