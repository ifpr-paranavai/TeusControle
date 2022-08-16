using Core.Shared.Models.Entry;
using FluentValidation;

namespace Manager.Validator.Entry
{
    public class CreateEntryValidator : AbstractValidator<CreateEntryModel>
    {
        public CreateEntryValidator()
        {
            RuleFor(x => x.Status).NotNull().NotEmpty();
            RuleFor(x => x.Origin).NotNull().NotEmpty();
        }
    }
}
