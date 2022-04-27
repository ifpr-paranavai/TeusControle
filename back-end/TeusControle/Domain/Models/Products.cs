using System.Collections.Generic;
using TeusControle.Domain.Models.CommonModels;

namespace TeusControle.Domain.Models
{
    public partial class Products : BaseEntity
    {
        public Products()
        {
            ProductEntries = new HashSet<ProductEntries>();
            ProductDisposals = new HashSet<ProductDisposals>();
        }
        /// <summary>
        /// Descrição
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Preço médio
        /// </summary>
        public decimal? AvgPrice { get; set; }

        /// <summary>
        /// Preço
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Preço máximo
        /// </summary>
        public decimal? MaxPrice { get; set; }

        /// <summary>
        /// Pesso bruto
        /// </summary>
        public decimal? GrossWeight { get; set; }

        /// <summary>
        /// Peso líquido
        /// </summary>
        public decimal? NetWeight { get; set; }

        /// <summary>
        /// Nome da marca
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// Imagem da marca
        /// </summary>
        public string BrandPicture { get; set; }

        /// <summary>
        /// Código Gpc
        /// </summary>
        public string GpcCode { get; set; }

        /// <summary>
        /// Descrição Gpc
        /// </summary>
        public string GpcDescription { get; set; }

        /// <summary>
        /// Código Gtin
        /// </summary>
        public string Gtin { get; set; }

        /// <summary>
        /// Altura
        /// </summary>
        public decimal? Height { get; set; }

        /// <summary>
        /// Comprimento
        /// </summary>
        public decimal? Lenght { get; set; }

        /// <summary>
        /// Largura
        /// </summary>
        public decimal? Width { get; set; }

        /// <summary>
        /// Código Ncm
        /// </summary>
        public string NcmCode { get; set; }

        /// <summary>
        /// Descrição Ncm
        /// </summary>
        public string NcmDescription { get; set; }

        /// <summary>
        /// Descrição completa do Ncm
        /// </summary>
        public string NcmFullDescription { get; set; }

        /// <summary>
        /// Imagem do produto
        /// </summary>
        public string Thumbnail { get; set; }

        /// <summary>
        /// Quantidade em estoque
        /// </summary>
        public decimal InStock { get; set; }

        /// <summary>
        /// Produto de uma entrada
        /// </summary>
        public ICollection<ProductEntries> ProductEntries { get; set; }

        /// <summary>
        /// Produto de uma saída
        /// </summary>
        public ICollection<ProductDisposals> ProductDisposals { get; set; }
    }
}
