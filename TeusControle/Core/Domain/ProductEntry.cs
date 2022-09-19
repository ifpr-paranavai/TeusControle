using Core.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain
{
    /// <summary>
    /// Tabela associativa de produtos para reistro de entrada de produtos
    /// </summary>
    public class ProductEntry : BaseDoubleEntity
    {
        /// <summary>
        /// Produto
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Registro da entrada de produto relacionada
        /// </summary
        public Entry Entry { get; set; }

        /// <summary>
        /// Quantidade de produtos a serem inseridos
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Preço unitário
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Preço total do produto
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal TotalPrice { get; private set; }
    }
}
