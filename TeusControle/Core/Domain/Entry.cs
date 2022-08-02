using Core.Domain.Base;
using Core.Shared.Models.Enums;
using System;
using System.Collections.Generic;

namespace Core.Domain
{
    /// <summary>
    /// Registro de entrada de produtos
    /// </summary>
    public class Entry : BaseEntity
    {
        public Entry()
        {
            ProductsEntry = new HashSet<ProductEntry>();
        }

        /// <summary>
        /// Origem da entrada
        /// </summary>
        public string Origin { get; set; }

        /// <summary>
        /// Data de fechamento
        /// </summary>
        public DateTime? ClosingDate { get; set; }

        /// <summary>
        /// Status do registro de entradas de produto
        /// </summary>
        public EntryStatusEnum Status { get; set; }

        /// <summary>
        /// Produto de uma entrada
        /// </summary>
        public ICollection<ProductEntry> ProductsEntry { get; set; }
    }
}
