// <copyright>
// Copyright (c) 2022 All Rights Reserved
// </copyright>
// <author>Sabyasachi Senapati</author>

using Core.Shared.Models.Enums;

namespace Core.Shared.Models.Request
{
    public class SortingParamsRequest
    {
        /// <summary>
        /// Tipo de ordenação
        /// </summary>
        public SortEnum SortOrder { get; set; } = SortEnum.Asc;

        /// <summary>
        /// Nome da coluna
        /// </summary>
        public string ColumnName { get; set; }
    }
}
