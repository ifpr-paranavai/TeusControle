using FluentValidation;
using TeusControle.Domain.Models;

namespace TeusControle.Application.Validators
{
    /// <summary>
    /// Validação para objeto do tipo saida de produtos
    /// </summary>
    public class DisposalsValidator : AbstractValidator<Disposals>
    {
        public DisposalsValidator()
        {
            RuleFor(c => c.PaymentType)
                .NotEmpty().WithMessage("Please enter the payment type.")
                .NotNull().WithMessage("Please enter the payment type.");
        }
    }
}
