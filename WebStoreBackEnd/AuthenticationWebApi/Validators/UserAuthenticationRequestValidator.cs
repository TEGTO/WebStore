using AuthenticationWebApi.Dtos.ControllerDtos;
using FluentValidation;

namespace AuthenticationWebApi.Validators
{
    public class UserAuthenticationRequestValidator : AbstractValidator<UserAuthenticationRequest>
    {
        public UserAuthenticationRequestValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotNull().NotEmpty().MinimumLength(8);
        }
    }
}