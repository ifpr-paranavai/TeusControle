// <copyright>
// Copyright (c) 2022 All Rights Reserved
// </copyright>
// <author>Sabyasachi Senapati</author>

using Core.Shared.Models.Enums;

namespace Core.Shared.Models.Request
{
    public class FilterParamsRequest
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