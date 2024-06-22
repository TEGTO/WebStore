using FluentValidation;
using WebStoreBackEnd.Models.Dto;

namespace WebStoreBackEnd.Validators
{
    public class AuthenticationModelDtoValidator : AbstractValidator<AuthenticationModelDto>
    {
        public AuthenticationModelDtoValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotNull().NotEmpty().MinimumLength(8);
        }
    }
}
