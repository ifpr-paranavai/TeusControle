using FluentValidation;
using TeusControle.Domain.Models;

namespace TeusControle.Application.Validators
{
    /// <summary>
    /// Validação para objeto do tipo usuário
    /// </summary>
    public class EntryValidator : AbstractValidator<Entries>
    {
        public EntryValidator()
        {
            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Please enter the description.")
                .NotNull().WithMessage("Please enter the description.");
        }
    }
}
