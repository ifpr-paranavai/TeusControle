using System;

namespace Core.Shared.Models.Product
{
    public class ProductPagedModel : ICloneable
    {
        /// <summary>
        /// Identificador
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Descrição
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Código de barras
        /// </summary>
        public string Gtin { get; set; }

        /// <summary>
        /// Marca
        /// </summary>
        public string BrandName { get; set; }

        public string InStock { get; set; }

        /// <summary>
        /// Preço
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Imagem do produto
        /// </summary>
        public string Thumbnail { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public bool Active { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
