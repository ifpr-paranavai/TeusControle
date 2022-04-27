using System.ComponentModel;

namespace TeusControle.Domain.Models.Enums
{
    public enum ProfileTypesEnum
    {
        /// <summary>
        /// 1 - Administrador
        /// </summary>
        [Description("Administrador")]
        Administrator = 1,

        /// <summary>
        /// 2 - Vendedor
        /// </summary>
        [Description("Vendedor")]
        Saler = 2,
    }
}
