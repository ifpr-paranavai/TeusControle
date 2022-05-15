using Core.Shared.Models.Product;
using FluentValidation;
using System;

namespace Manager.Validator.Product
{
    public class CreateProductValidator : AbstractValidator<CreateProductModel>
    {
        public CreateProductValidator()
        {
            
        }
    }
}
