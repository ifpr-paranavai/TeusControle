namespace Core.Shared.Models.Sale
{
    /// <summary>
    /// Objeto para alteração de venda
    /// </summary>
    public class UpdateSaleModel : CreateSaleModel
    {
        /// <summary>
        /// Id da venda
        /// </summary>
        /// <example>123</example>
        public int Id { get; set; }

        /// <summary>
        /// Status do registro
        /// </summary>
        /// <example>true</example>
        public bool Active { get; set; }
    }
}
