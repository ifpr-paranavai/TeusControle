namespace TeusControle.Domain.Models.Dtos
{
    /// <summary>
    /// Dto para dar entrada de um produto
    /// </summary>
    public class CreateProductEntriesModel
    {
        /// <summary>
        /// Identificador da entrada correspondente
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Identificador do produto
        /// </summary>
        public long Id2 { get; set; }

        /// <summary>
        /// Quantidade de produtos a serem adicionados
        /// </summary>
        public long Amount { get; set; }
    }

    /// <summary>
    /// Dto para atualizar a entrada de um produto
    /// </summary>
    public class UpdateProductEntriesModel : CreateProductEntriesModel
    {
        /// <summary>
        /// Está ativo?
        /// </summary>
        public bool Active { get; set; }
    }

    /// <summary>
    /// Dto com informações da entrada de um produto
    /// </summary>
    public class ProductEntriesModel : UpdateProductEntriesModel
    {

    }
}
