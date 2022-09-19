using Core.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain
{
    /// <summary>
    /// Tabela associativa de produtos para registro de venda de produtos
    /// </summary>
    public class ProductSale : BaseDoubleEntity
    {
        /// <summary>
        /// Produto
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Registro da venda de produto relacionada
        /// </summary
        public Sale Sale { get; set; }

        /// <summary>
        /// Quantidade de produtos vendidos
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Preço unitario do produto
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Preço unitário aplicando desconto
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal UnitOutPrice { get; set; }

        /// <summary>
        /// Preço total [quantidade * preço unitario]
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal TotalPrice { get; private set; }

        /// <summary>
        /// Preço total [quantidade * preço unitario com desconto]
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal TotalOutPrice { get; private set; }

        /// <summary>
        /// Desconto em valor monetário
        /// Padrão é ZERO
        /// </summary>
        public decimal Discount { get; set; }
    }
}
