using Bogus;
using Core.Domain;

namespace FakeData.ProductData
{
    public class ProductFake : Faker<Product>
    {
        public ProductFake()
        {
            RuleFor(p => p.Description, f => f.Commerce.ProductName());
            RuleFor(p => p.Gtin, f => f.Random.Int(100000, 900000).ToString());
            RuleFor(p => p.Price, f => f.Random.Decimal(100, 500));
            RuleFor(p => p.InStock, f => f.Random.Number(50, 500));
            RuleFor(p => p.Active, f => f.Random.Bool());
            RuleFor(p => p.CreatedDate, f => f.Date.Recent(3));
            RuleFor(p => p.LastChange, f => f.Date.Recent(0));
            RuleFor(p => p.Deleted, f => false);
            RuleFor(p => p.LastChange, f => f.Date.Past());
            RuleFor(p => p.CreatedDate, f => f.Date.Past());
        }
    }
}
