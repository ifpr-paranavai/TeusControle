using System;
using System.Collections.Generic;
using TeusControle.Domain.Models.CommonModels;
using TeusControle.Domain.Models.Enums;

namespace TeusControle.Domain.Models
{
    /// <summary>
    /// Registro de saída de produtos
    /// </summary>
    public class Disposals : BaseEntity
    {
        public Disposals()
        {
            ProductDisposals = new HashSet<ProductDisposals>();
        }

        /// <summary>
        /// Cpf ou Cnpj
        /// </summary>
        public String CpfCnpj { get; set; }

        /// <summary>
        /// Tipo de pagamento
        /// </summary>
        public PaymentTypesEnum PaymentType { get; set; }

        /// <summary>
        /// Tipo de saida
        /// </summary>
        public DisposalTypesEnum DisposalType { get; set; }

        /// <summary>
        /// Situação da saída
        /// </summary>
        public DisposalsStatusEnum Status { get; set; }

        /// <summary>
        /// Data de fechamento
        /// </summary>
        public DateTime? ClosingDate { get; set; }

        /// <summary>
        /// Produtos de uma saída
        /// </summary>
        public ICollection<ProductDisposals> ProductDisposals { get; set; }
    }
}
