using Core.Shared.Models.User;
using FluentValidation;

namespace Manager.Validator.User
{
    public class UpdateUsersValidator : AbstractValidator<UpdateUserModel>
    {
        public UpdateUsersValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().GreaterThan(0);
            Include(new CreateUsersValidator());
        }
    }
}
