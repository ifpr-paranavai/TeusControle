using Bogus;
using Core.Shared.Models.Product;

namespace FakeData.ProductData
{
    public class UpdateProductModelFake : Faker<UpdateProductModel>
    {
        public UpdateProductModelFake()
        {
            RuleFor(p => p.Description, f => f.Commerce.ProductName());
            RuleFor(p => p.Gtin, f => f.Random.Int(100000, 900000).ToString());
            RuleFor(p => p.Price, f => f.Random.Decimal(100, 500));
            RuleFor(p => p.InStock, f => f.Random.Number(50, 500));
            RuleFor(p => p.Active, f => true);
        }
    }
}
