using FluentValidation;
using JWTAuth.Entities.Dto;

namespace JWTAuth.Business.Validations.FluentValidation
{
    public class UserForEditDtoValidator : AbstractValidator<UserForEditDto>
    {
        public UserForEditDtoValidator()
        {
            RuleFor(request => request.Name).NotEmpty();
        }
    }
}
