using Core.Domain.Base;

namespace Core.Domain
{
    /// <summary>
    /// Tabela associativa de produtos para tabela de entrada de produtos
    /// </summary>
    public class ProductEntry : BaseDoubleEntity
    {
        /// <summary>
        /// Produto
        /// </summary>
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        /// <summary>
        /// Registro da entrada de produto relacionada
        /// </summary>
        public int EntryId { get; set; }
        public virtual Entry Entry { get; set; }

        /// <summary>
        /// Quantidade de produtos a serem inseridos
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
    }
}
