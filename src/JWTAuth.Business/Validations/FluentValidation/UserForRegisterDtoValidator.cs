using FluentValidation;
using JWTAuth.Entities;

namespace JWTAuth.Business.Validations.FluentValidation
{
    public class UserForRegisterDtoValidator : AbstractValidator<UserForRegisterDto>
    {
        public UserForRegisterDtoValidator()
        {
            RuleFor(request => request.Email).EmailAddress().NotEmpty();
            RuleFor(request => request.Password)
                    .NotEmpty()
                    .MinimumLength(8)
                    .Equal(request => request.ConfirmPassword).WithMessage("Parolalar Uyuşmuyor");
            RuleFor(request => request.UserName).NotEmpty();
            RuleFor(request => request.Name).NotEmpty();
            RuleFor(request => request.ConfirmPassword).NotEmpty();

        }
    }
}
