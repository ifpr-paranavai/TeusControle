using System.ComponentModel;

namespace Core.Shared.Models.Enums
{
    public enum SaleStatusEnum
    {
        /// <summary>
        /// 0 - Aberto
        /// </summary>
        [Description("Aberto")]
        Opened = 1,

        /// <summary>
        /// 2 - Fechado
        /// </summary>
        [Description("Fechado")]
        Closed = 2,

        /// <summary>
        /// 3 - Cancelado
        /// </summary>
        [Description("Cancelado")]
        Canceled = 3,

        /// <summary>
        /// 4 - Pendente
        /// </summary>
        [Description("Pendente")]
        Pending = 4
    }
}
