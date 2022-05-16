using Core.Shared.Models.Product;
using FluentValidation;

namespace Manager.Validator.Product
{
    public class CreateProductValidator : AbstractValidator<CreateProductModel>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Description).NotNull().NotEmpty();
            RuleFor(x => x.Gtin).NotNull().NotEmpty();
            RuleFor(x => x.Price).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.InStock).NotNull().NotEmpty().GreaterThan(0);
        }
    }
}
