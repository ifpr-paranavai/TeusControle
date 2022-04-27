using System.ComponentModel;

namespace TeusControle.Domain.Models.Enums
{
    public enum PaymentTypesEnum
    {
        /// <summary>
        /// 1 - Dinheiro
        /// </summary>
        [Description("Dinheiro")]
        Money = 1,

        /// <summary>
        /// 2 - Cartão de crédito
        /// </summary>
        [Description("Cartão de crédito")]
        CreditCard = 2,

        /// <summary>
        /// 3 - Cartão de débito
        /// </summary>
        [Description("Cartão de débito")]
        DebitCart = 3,

        /// <summary>
        /// 4 - Pix
        /// </summary>
        [Description("Pix")]
        Pix = 4,

        /// <summary>
        /// 4 - À prazo
        /// </summary>
        [Description("À prazo")]
        OnTimes = 5,
    }
}
