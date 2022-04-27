namespace TeusControle.Domain.Models.Dtos
{
    /// <summary>
    /// Dto para cadastrar um novo produto
    /// </summary>
    public class CreateProductsModel
    {
        /// <summary>
        /// Descrição
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Preço médio
        /// </summary>
        public decimal AvgPrice { get; set; }

        /// <summary>
        /// Preço
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Preço máximo
        /// </summary>
        public decimal MaxPrice { get; set; }

        /// <summary>
        /// Pesso bruto
        /// </summary>
        public decimal GrossWeight { get; set; }

        /// <summary>
        /// Peso líquido
        /// </summary>
        public int NetWeight { get; set; }

        /// <summary>
        /// Nome da marca
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// Imagem da marca
        /// </summary>
        public string BrandPicture { get; set; }

        /// <summary>
        /// Código Gpc
        /// </summary>
        public string GpcCode { get; set; }

        /// <summary>
        /// Descrição Gpc
        /// </summary>
        public int GpcDescription { get; set; }

        /// <summary>
        /// Código Gtin
        /// </summary>
        public long Gtin { get; set; }

        /// <summary>
        /// Altura
        /// </summary>
        public decimal Height { get; set; }

        /// <summary>
        /// Comprimento
        /// </summary>
        public decimal Lenght { get; set; }

        /// <summary>
        /// Largura
        /// </summary>
        public decimal Width { get; set; }

        /// <summary>
        /// Código Ncm
        /// </summary>
        public string NcmCode { get; set; }

        /// <summary>
        /// Descrição Ncm
        /// </summary>
        public string NcmDescription { get; set; }

        /// <summary>
        /// Descrição completa do Ncm
        /// </summary>
        public string NcmFullDescription { get; set; }

        /// <summary>
        /// Imagem do produto
        /// </summary>
        public string Thumbnail { get; set; }

        /// <summary>
        /// Quantidade em estoque
        /// </summary>
        public decimal InStock { get; set; }
    }

    /// <summary>
    /// Dto para atualizar um produto
    /// </summary>
    public class UpdateProductsModel : CreateProductsModel
    {
        /// <summary>
        /// Identificador
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Está ativo?
        /// </summary>
        public bool Active { get; set; }
    }

    /// <summary>
    /// Dto com informações do produto
    /// </summary>
    public class ProductsModel : UpdateProductsModel
    {

    }
}
