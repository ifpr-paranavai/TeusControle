using System;

namespace Core.Shared.Models.Entry
{
    public class EntryPagedModel : ICloneable
    {
        /// <summary>
        /// Identificador
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Origem
        /// </summary>
        public string Origin { get; set; }

        /// <summary>
        /// Status da entrada
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Data de fechamento da entrada
        /// </summary>
        public string ClosingDate { get; set; }

        /// <summary>
        /// Preço total da entrada
        /// </summary>
        public decimal TotalPrice { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
