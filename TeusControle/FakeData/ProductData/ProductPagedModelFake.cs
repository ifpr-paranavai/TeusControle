using Bogus;
using Core.Shared.Models.Product;

namespace FakeData.ProductData
{
    public class ProductPagedModelFake : Faker<ProductPagedModel>
    {
        public ProductPagedModelFake()
        {
            RuleFor(p => p.Id, f => new Faker().Random.Number(1, 50));
            RuleFor(p => p.Description, f => f.Commerce.ProductName());
            RuleFor(p => p.Gtin, f => f.Random.Int(100000, 900000).ToString());
            RuleFor(p => p.Price, f => f.Random.Decimal(100, 500));
            RuleFor(p => p.InStock, f => f.Random.Number(50, 500).ToString());
            RuleFor(p => p.Thumbnail, f => f.Image.PicsumUrl());
            RuleFor(p => p.BrandName, f => f.Commerce.Department());
            RuleFor(p => p.Active, f => true);
        }
    }
}
