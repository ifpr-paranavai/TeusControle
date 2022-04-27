using System.Collections.Generic;

namespace TeusControle.Infrastructure.Dtos
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
