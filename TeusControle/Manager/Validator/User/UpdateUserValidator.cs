using Core.Shared.Models.User;
using FluentValidation;

namespace Manager.Validator.User
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserModel>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().GreaterThan(0);
            Include(new CreateUserValidator());
        }
    }
}
