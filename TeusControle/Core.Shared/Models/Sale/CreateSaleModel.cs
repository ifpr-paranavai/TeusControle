using Core.Shared.Models.Enums;
using System.Collections.Generic;

namespace Core.Shared.Models.Sale
{
    /// <summary>
    /// Objeto para criação de uma nova venda
    /// </summary>
    public class CreateSaleModel
    {
        /// <summary>
        /// CpfCnpj
        /// </summary>
        public string CpfCnpj { get; set; }

        /// <summary>
        /// Status da venda
        /// </summary>
        public SaleStatusEnum Status { get; set; }

        /// <summary>
        /// Produtos da venda
        /// </summary>
        public ICollection<ProductSaleModel> Products { get; set; }

/*        /// <summary>
        /// Total do desconto em valor monetário
        /// </summary>
        public decimal TotalDiscount { get; set; }*/
    }

    public class ProductSaleModel
    {
        /// <summary>
        /// Id do produto
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Quantidade vendida
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Preço pago por unidade
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Desconto em valor monetário
        /// </summary>
        public decimal Discount { get; set; }
    }
}
