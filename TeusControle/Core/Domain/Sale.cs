using Core.Domain.Base;
using Core.Shared.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain
{
    /// <summary>
    /// Registro de venda de produtos
    /// </summary>
    public class Sale : BaseEntity
    {
        public Sale()
        {
            ProductsSale = new HashSet<ProductSale>();
            TotalPrice = 0;
        }

        /// <summary>
        /// Data de fechamento
        /// </summary>
        public DateTime? ClosingDate { get; set; }

        /// <summary>
        /// Status do registro da venda de produto
        /// </summary>
        public SaleStatusEnum Status { get; set; }

        /// <summary>
        /// Documento do cliente
        /// </summary>
        public string CpfCnpj { get; set; }

        /// <summary>
        /// Produtos de uma saída
        /// </summary>
        public ICollection<ProductSale> ProductsSale { get; set; }

        /// <summary>
        /// Soma de todos os valores dos produtos da venda
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal TotalPrice { get; private set; }

        /// <summary>
        /// Valor da venda aplicando desconto
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal TotalOutPrice { get; set; }

        /// <summary>
        /// Valor do desconto concedido 
        /// </summary>
        public decimal TotalDiscount { get; set; }
    }
}
