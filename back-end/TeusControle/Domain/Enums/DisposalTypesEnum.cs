using System.ComponentModel;

namespace TeusControle.Domain.Models.Enums
{
    public enum DisposalTypesEnum
    {
        /// <summary>
        /// 1 - Venda
        /// </summary>
        [Description("Venda")]
        Sale = 1,

        /// <summary>
        /// 2 - Descartar
        /// </summary>
        [Description("Descartar")]
        Discard = 2
    }
}
