using System.Collections.Generic;

namespace Core.Shared.Models.Request
{
    /// <summary>  
    /// Propriedades usadas para paginação, ordenação e filtro
    /// </summary>  
    public class PaginatedRequest
    {
        /// <summary>
        /// Parâmetros de ordenação
        /// </summary>
        public IEnumerable<SortingParamsRequest> SortingParams { set; get; }

        /// <summary>
        /// Parâmetros de filtragem
        /// </summary>
        public IEnumerable<FilterParamsRequest> FilterParams { get; set; }

        /*/// <summary>
        /// 
        /// </summary>
        public IEnumerable<string> GroupingColumns { get; set; } = null;*/

        /// <summary>
        /// Número da página
        /// </summary>
        int pageNumber = 1;

        /// <summary>
        /// Número da página
        /// </summary>
        public int PageNumber
        {
            get { return pageNumber; }
            set { if (value > 1) pageNumber = value; }
        }

        /// <summary>
        /// Tamanho da página
        /// </summary>
        int pageSize = 25;

        /// <summary>
        /// Tamanho da página
        /// </summary>
        public int PageSize
        {
            get { return pageSize; }
            set { if (value > 1) pageSize = value; }
        }
    }
}
