using FluentValidation;
using WebStoreBackEnd.Models.Dto;

namespace WebStoreBackEnd.Validators
{
    public class UserAuthenticationDtoValidator : AbstractValidator<UserAuthenticationDto>
    {
        public UserAuthenticationDtoValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotNull().NotEmpty().MinimumLength(8);
        }
    }
}
