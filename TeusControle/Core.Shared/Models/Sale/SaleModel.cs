using System;

namespace Core.Shared.Models.Sale
{
    /// <summary>
    /// Objeto de retorno de venda
    /// </summary>
    public class SaleModel : UpdateSaleModel, ICloneable
    {
        /// <summary>
        /// Data e hora de criação do registro
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Data e hora da última atualização do registro
        /// </summary>
        public DateTime? LastChange { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public SaleModel CloneTyped()
        {
            return (SaleModel)Clone();
        }
    }
}
