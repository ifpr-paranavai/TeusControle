// <copyright>
// Copyright (c) 2022 All Rights Reserved
// </copyright>
// <author>Sabyasachi Senapati</author>

using System.Collections.Generic;

namespace Core.Shared.Models.Responses
{
    /// <summary>
    /// Retorno para busca páginada
    /// </summary>
    public class PageResponse
    {
        /// <summary>
        /// Informações
        /// </summary>
        public IEnumerable<object> Data { get; set; }

        /// <summary>
        /// Quantidade de registros
        /// </summary>
        public long Count { get; set; }
    }
}
