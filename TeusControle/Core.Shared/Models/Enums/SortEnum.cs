using System.ComponentModel;

namespace Core.Shared.Models.Enums
{
    /// <summary>
    /// Enumeradores para ordenação
    /// </summary>
    public enum SortEnum
    {
        /// <summary>
        /// 1 - Ascendente
        /// </summary>
        [Description("Ascendente")]
        Asc = 1,

        /// <summary>
        /// 2 - Descendente
        /// </summary>
        [Description("Descendente")]
        Desc = 2
    }
}
