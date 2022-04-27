using System.Collections.Generic;
using TeusControle.Domain.Models.CommonModels;

namespace TeusControle.Domain.Models
{
    /// <summary>
    /// Tabela associativa de produtos para tabela de entrada de produtos
    /// </summary>
    public class ProductEntries : BaseDoubleEntity
    {
        /// <summary>
        /// Produto
        /// </summary>
        public Products Product { get; set; }

        /// <summary>
        /// Registro da entrada de produto relacionada
        /// </summary>
        public Entries Entry { get; set; }

        /// <summary>
        /// Quantidade de produtos a serem inseridos
        /// </summary>
        public decimal Amount { get; set; }
    }
}
