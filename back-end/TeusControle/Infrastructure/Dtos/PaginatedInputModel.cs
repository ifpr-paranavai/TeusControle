using System.Collections.Generic;

namespace TeusControle.Infrastructure.Dtos
{
    /// <summary>  
    /// This class contains properites used for paging, sorting, grouping, filtering and will be used as a parameter model  
    ///   
    /// SortOrder   - enum of sorting orders  
    /// SortColumn  - Name of the column on which sorting has to be done,  
    ///               as for now sorting can be performed only on one column at a time.  
    /// FilterParams - Filtering can be done on multiple columns and for one column multiple values can be selected  
    ///               key :- will be column name, Value :- will be array list of multiple values  
    /// PageNumber   - Page Number to be displayed in UI, default to 1  
    /// PageSize     - Number of items per page, default to 25  
    /// </summary>  
    public class PaginatedInputModel
    {
        /// <summary>
        /// Parâmetros de ordenação
        /// </summary>
        public IEnumerable<SortingParams> SortingParams { set; get; }

        /// <summary>
        /// Parâmetros de filtragem
        /// </summary>
        public IEnumerable<FilterParams> FilterParams { get; set; }

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
