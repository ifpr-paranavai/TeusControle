using Core.Shared.Models.Entry;
using FluentValidation;

namespace Manager.Validator.Entry
{
    public class UpdateEntryValidator : AbstractValidator<UpdateEntryModel>
    {
        public UpdateEntryValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().GreaterThan(0);
            Include(new CreateEntryValidator());
        }
    }
}
