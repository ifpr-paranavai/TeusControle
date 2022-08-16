using Core.Shared.Models.Enums;
using System.Collections.Generic;

namespace Core.Shared.Models.Entry
{
    /// <summary>
    /// Objeto para criação de uma novo entrada
    /// </summary>
    public class CreateEntryModel
    {
        /// <summary>
        /// Origem
        /// </summary>
        public string Origin { get; set; }

        /// <summary>
        /// Status da entrada
        /// </summary>
        public EntryStatusEnum Status { get; set; }

        /// <summary>
        /// Produtos da entrada
        /// </summary>
        public ICollection<ProductEntryModel> Products { get; set; }

        /*/// <summary>
        /// Data de fechamento da entrada
        /// </summary>
        public string ClosingDate { get; set; }*/
    }

    public class ProductEntryModel
    {
        /// <summary>
        /// Id do produto
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Quantidade entrando
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Preço pago por unidade
        /// </summary>
        public decimal UnitPrice { get; set; }
    }
}
