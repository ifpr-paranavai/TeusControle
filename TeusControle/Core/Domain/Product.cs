using Core.Domain.Base;

namespace Core.Domain
{
    public class Product : BaseEntity
    {
        public Product()
        {
            /*ProductEntries = new HashSet<ProductEntries>();
            ProductDisposals = new HashSet<ProductDisposals>();*/
        }

        public string Description { get; set; }

        public decimal? AvgPrice { get; set; }

        public decimal Price { get; set; }
        
        public decimal? MaxPrice { get; set; }

        public decimal? GrossWeight { get; set; }

        public decimal? NetWeight { get; set; }

        public string BrandName { get; set; }

        public string BrandPicture { get; set; }

        public string GpcCode { get; set; }

        public string GpcDescription { get; set; }

        public string Gtin { get; set; }

        public decimal? Height { get; set; }

        public decimal? Lenght { get; set; }

        public decimal? Width { get; set; }

        public string NcmCode { get; set; }

        public string NcmDescription { get; set; }

        public string NcmFullDescription { get; set; }

        public string Thumbnail { get; set; }

        public decimal InStock { get; set; }

        /*public ICollection<ProductEntries> ProductEntries { get; set; }*/

        /*public ICollection<ProductDisposals> ProductDisposals { get; set; }*/
    }
}
