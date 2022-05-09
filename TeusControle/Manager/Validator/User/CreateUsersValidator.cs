using Core.Shared.Models.User;
using FluentValidation;
using System;

namespace Manager.Validator.User
{
    public class CreateUsersValidator : AbstractValidator<CreateUserModel>
    {
        public CreateUsersValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(10).MaximumLength(200);
            RuleFor(x => x.CpfCnpj).NotNull().NotEmpty().MinimumLength(11).MaximumLength(14);
            RuleFor(x => x.DocumentType).NotNull().NotEmpty();
            RuleFor(x => x.BirthDate).NotNull().NotEmpty().LessThan(DateTime.Now).GreaterThan(DateTime.Now.AddYears(-130));
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotNull().NotEmpty();
        }
    }
}
