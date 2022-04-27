using System;
using System.Collections.Generic;
using TeusControle.Domain.Models.CommonModels;
using TeusControle.Domain.Models.Enums;

namespace TeusControle.Domain.Models
{
    /// <summary>
    /// Registro de entrada de produtos
    /// </summary>
    public class Entries : BaseEntity
    {
        public Entries()
        {
            ProductEntries = new HashSet<ProductEntries>();
        }

        /// <summary>
        /// Descrição, título da entrada
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Data de fechamento
        /// </summary>
        public DateTime? ClosingDate { get; set; }

        /// <summary>
        /// Status do registro de entradas de produto
        /// </summary>
        public EntriesStatusEnum Status { get; set; }

        /// <summary>
        /// Produto de uma entrada
        /// </summary>
        public ICollection<ProductEntries> ProductEntries { get; set; }
    }
}
