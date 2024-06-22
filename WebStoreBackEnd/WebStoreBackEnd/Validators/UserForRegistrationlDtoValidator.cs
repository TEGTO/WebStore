﻿using FluentValidation;
using WebStoreBackEnd.Models.Dto;

namespace WebStoreBackEnd.Validators
{
    public class UserForRegistrationlDtoValidator : AbstractValidator<UserRegistrationlDto>
    {
        public UserForRegistrationlDtoValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotNull().NotEmpty().MinimumLength(8);
            RuleFor(x => x.ConfirmPassword).Must((model, field) => field == model.Password)
                .WithMessage("Passwords do not match.");
        }
    }
}
