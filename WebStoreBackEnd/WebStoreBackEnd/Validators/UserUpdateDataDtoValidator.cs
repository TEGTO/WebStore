using FluentValidation;
using WebStoreBackEnd.Models.Dto;

namespace WebStoreBackEnd.Validators
{
    public class UserUpdateDataDtoValidator : AbstractValidator<UserUpdateDataDto>
    {
        public UserUpdateDataDtoValidator()
        {
            RuleFor(x => x.OldEmail).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.NewEmail).EmailAddress().When(x => !string.IsNullOrEmpty(x.NewEmail));
            RuleFor(x => x.OldPassword).NotNull().NotEmpty().MinimumLength(8);
            RuleFor(x => x.NewPassword).MinimumLength(8).When(x => !string.IsNullOrEmpty(x.NewPassword));
        }
    }
}
