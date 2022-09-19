using Core.Domain.Base;
using System.Collections.Generic;

namespace Core.Domain
{
    public class Product : BaseEntity
    {
        public Product()
        {
            ProductsEntry = new HashSet<ProductEntry>();
            ProductsSale = new HashSet<ProductSale>();
        }

        public string Description { get; set; }

        public decimal? AvgPrice { get; set; }

        public decimal Price { get; set; }

        public string BrandName { get; set; }

        public string BrandPicture { get; set; }

        public string GpcCode { get; set; }

        public string GpcDescription { get; set; }

        public string Gtin { get; set; }

        public string NcmCode { get; set; }

        public string NcmDescription { get; set; }

        public string NcmFullDescription { get; set; }

        public string Thumbnail { get; set; }

        public decimal InStock { get; set; }

        public ICollection<ProductEntry> ProductsEntry { get; set; }

        public ICollection<ProductSale> ProductsSale{ get; set; }
    }
}
