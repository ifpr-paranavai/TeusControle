using System;

namespace Core.Shared.Models.Product
{
    /// <summary>
    /// Objeto de retorno de produto
    /// </summary>
    public class ProductModel : UpdateProductModel
    {
        /// <summary>
        /// Data e hora de criação do registro
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Data e hora da última atualização do registro
        /// </summary>
        public DateTime? LastChange { get; set; }
    }
}
