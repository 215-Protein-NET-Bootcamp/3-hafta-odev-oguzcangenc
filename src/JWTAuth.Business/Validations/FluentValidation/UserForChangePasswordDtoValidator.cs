using FluentValidation;
using JWTAuth.Entities;

namespace JWTAuth.Business.Validations.FluentValidation
{
    public class UserForChangePasswordDtoValidator : AbstractValidator<UserForChangePasswordDto>
    {
        public UserForChangePasswordDtoValidator()
        {
            RuleFor(request => request.OldPassword).NotEmpty();
            RuleFor(request => request.NewPassword).NotEmpty().Equal(request => request.ConfirmNewPassword).WithMessage("Yeni Parolalar Uyuşmuyor");
            RuleFor(request => request.ConfirmNewPassword).NotEmpty();


        }
    }
}
