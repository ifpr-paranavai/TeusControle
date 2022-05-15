using Core.Shared.Models.Product;
using FluentValidation;

namespace Manager.Validator.Product
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductModel>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().GreaterThan(0);
            Include(new CreateProductValidator());
        }
    }
}
