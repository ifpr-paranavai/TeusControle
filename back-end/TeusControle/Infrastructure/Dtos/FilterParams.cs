using TeusControle.Infrastructure.Enums;

namespace TeusControle.Infrastructure.Dtos
{
    public class FilterParams
    {
        /// <summary>
        /// Nome da coluna a ser filtrada
        /// </summary>
        public string ColumnName { get; set; } = string.Empty;

        /// <summary>
        /// Valor buscado no filtro
        /// </summary>
        public string FilterValue { get; set; } = string.Empty;

        /// <summary>
        /// Tipo do filtro a ser aplicado
        /// </summary>
        public FilterEnum FilterOption { get; set; } = FilterEnum.Contains;
    }
}