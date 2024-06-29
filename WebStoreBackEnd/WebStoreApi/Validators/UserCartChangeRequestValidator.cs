using FluentValidation;
using WebStoreApi.Models.Dto;

namespace AuthenticationWebApi.Validators
{
    public class UserCartChangeRequestValidator : AbstractValidator<UserCartChangeRequest>
    {
        public UserCartChangeRequestValidator()
        {
            RuleFor(x => x.UserEmail).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.ProductId).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Amount).GreaterThanOrEqualTo(0);
        }
    }
}
