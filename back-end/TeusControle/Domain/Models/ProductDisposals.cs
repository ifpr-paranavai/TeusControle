using TeusControle.Domain.Models.CommonModels;

namespace TeusControle.Domain.Models
{
    /// <summary>
    /// Tabela associativa de produtos para tabela de saída de produtos
    /// </summary>
    public class ProductDisposals : BaseDoubleEntity
    {
        /// <summary>
        /// Produto
        /// </summary>
        public Products Product { get; set; }

        /// <summary>
        /// Registro da saída de produto relacionada
        /// </summary>
        public Disposals Disposal { get; set; }

        /// <summary>
        /// Quantidade de produtos a sairem do estoque
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Preço unitário
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Preço total
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Preço líquido
        /// </summary>
        public decimal LiquidPrice { get; set; }

        /// <summary>
        /// Porcentagem de desconto
        /// </summary>
        public int PercentualDiscount { get; set; }

    }
}
