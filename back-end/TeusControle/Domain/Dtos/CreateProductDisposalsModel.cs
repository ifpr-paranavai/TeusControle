namespace TeusControle.Domain.Models.Dtos
{
    /// <summary>
    /// Dto para dar entrada de um produto
    /// </summary>
    public class CreateProductDisposalsModel
    {
        /// <summary>
        /// Identificador da saída correspondente
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Identificador do produto
        /// </summary>
        public long Id2 { get; set; }

        /// <summary>
        /// Quantidade de produtos a serem adicionados
        /// </summary>
        public long Amount { get; set; }

        /// <summary>
        /// Preço unitário
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Porcentagem de desconto
        /// </summary>
        public int PercentualDiscount { get; set; }
    }

    /// <summary>
    /// Dto para atualizar a entrada de um produto
    /// </summary>
    public class UpdateProductDisposalsModel : CreateProductDisposalsModel
    {
        /// <summary>
        /// Está ativo?
        /// </summary>
        public bool Active { get; set; }
    }

    /// <summary>
    /// Dto com informações da entrada de um produto
    /// </summary>
    public class ProductDisposalsModel : UpdateProductDisposalsModel
    {
        /// <summary>
        /// Preço total
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Preço líquido
        /// </summary>
        public decimal LiquidPrice { get; set; }
    }
}
