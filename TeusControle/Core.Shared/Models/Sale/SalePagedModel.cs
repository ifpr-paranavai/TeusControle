using System;

namespace Core.Shared.Models.Sale
{
    public class SalePagedModel : ICloneable
    {
        /// <summary>
        /// Identificador
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Cpf ou Cnpj do cliente
        /// </summary>
        public string CpfCnpj { get; set; }

        /// <summary>
        /// Status da venda
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Data de fechamento da venda
        /// </summary>
        public string ClosingDate { get; set; }

        /// <summary>
        /// Preço total da venda
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Preço total da venda com desconto
        /// </summary>
        public decimal TotalOutPrice { get; set; }

        /// <summary>
        /// Desconto
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Deve permitir exclusão?
        /// </summary>
        public bool CanBeDeleted { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
