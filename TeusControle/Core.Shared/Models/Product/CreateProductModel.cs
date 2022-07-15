using Core.Shared.Models.Enums;
using System;

namespace Core.Shared.Models.Product
{
    /// <summary>
    /// Objeto para criação de um novo produto
    /// </summary>
    public class CreateProductModel
    {
        /// <summary>
        /// Descrição
        /// </summary>
        /// <example>GRAMPO 26-6 CAIXA C-5000 IRPEL COBR 01</example>
        public string Description { get; set; }

        /// <summary>
        /// Preço médio
        /// </summary>
        /// <example>10.05</example>
        public decimal? AvgPrice { get; set; }

        /// <summary>
        /// Preço
        /// </summary>
        /// <example>10.55</example>
        public decimal Price { get; set; }

        /// <summary>
        /// Nome da marca
        /// </summary>
        /// <example>STR MICRONS</example>
        public string BrandName { get; set; }

        /// <summary>
        /// Imagem da marca
        /// </summary>
        /// <example>https://ciclovivo.com.br/wp-content/uploads/2018/10/iStock-536613027-696x464.jpg</example>
        public string BrandPicture { get; set; }

        /// <summary>
        /// Codigo da classificação global do produto (Global Product Classification)
        /// </summary>
        /// <example>10000043</example>
        public string GpcCode { get; set; }

        /// <summary>
        /// Descrição da classificação global do produto (Global Product Classification)
        /// </summary>
        /// <example>Açúcar / Substitutos do Açúcar (Não perecível)</example>
        public string GpcDescription { get; set; }

        /// <summary>
        /// Código de barras
        /// </summary>
        /// <example>7891910000197</example>
        public string Gtin { get; set; }

        /// <summary>
        /// Código NCM
        /// Definem as alíquotas de impostos no comércio exterior e de diversos tributos internos
        /// </summary>
        /// <example>83052000</example>
        public string NcmCode { get; set; }

        /// <summary>
        /// Descrição do NCM
        /// </summary>
        /// <example>Grampos apresentados em barretas</example>
        public string NcmDescription { get; set; }

        /// <summary>
        /// Descrição completa do NCM
        /// </summary>
        /// <example>Obras diversas de metais comuns - Ferragens para encadernação de folhas 
        /// móveis ou para classificadores, molas para papéis, cantos para cartas, clipes...</example>
        public string NcmFullDescription { get; set; }

        /// <summary>
        /// Imagem do produto
        /// </summary>
        /// <example>https://ciclovivo.com.br/wp-content/uploads/2018/10/iStock-536613027-696x464.jpg</example>
        public string Thumbnail { get; set; }

        /// <summary>
        /// Quantidade em estoque
        /// </summary>
        /// <example>10</example>
        public decimal InStock { get; set; }
    }
}
