namespace Core.Shared.Models.Product
{
    /// <summary>
    /// Objeto para alteração de um produto
    /// </summary>
    public class UpdateProductModel : CreateProductModel
    {
        /// <summary>
        /// Id do produto
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
