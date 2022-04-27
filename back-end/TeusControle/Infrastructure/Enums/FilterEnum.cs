using System.ComponentModel;

namespace TeusControle.Infrastructure.Enums
{
    public enum FilterEnum
    {
        /// <summary>
        /// 1 - Começa com
        /// </summary>
        [Description("Começa com")]
        StartsWith = 1,

        /// <summary>
        /// 2 - Termina com
        /// </summary>
        [Description("Termina com")]
        EndsWith = 2,

        /// <summary>
        /// 3 - Contém
        /// </summary>
        [Description("Contém")]
        Contains = 3,

        /// <summary>
        /// 4 - Não Contém
        /// </summary>
        [Description("Não Contém")]
        DoesNotContain = 4,

        /// <summary>
        /// 5 - Está Vazio
        /// </summary>
        [Description("Está Vazio")]
        IsEmpty = 5,

        /// <summary>
        /// 6 - Não Está Vazio
        /// </summary>
        [Description("Não Está Vazio")]
        IsNotEmpty = 6,

        /// <summary>
        /// 7 - É Maior
        /// </summary>
        [Description("É Maior")]
        IsGreaterThan = 7,

        /// <summary>
        /// 8 - É Maior ou Igual
        /// </summary>
        [Description("É Maior ou Igual")]
        IsGreaterThanOrEqualTo = 8,

        /// <summary>
        /// 9 - É Menor
        /// </summary>
        [Description("É Menor")]
        IsLessThan = 9,

        /// <summary>
        /// 10 - É Menor ou Igual
        /// </summary>
        [Description("É Menor ou Igual")]
        IsLessThanOrEqualTo = 10,

        /// <summary>
        /// 11 - É Igual
        /// </summary>
        [Description("É Igual")]
        IsEqualTo = 11,

        /// <summary>
        /// 12 - Ñão é Igual
        /// </summary>
        [Description("Não é Igual")]
        IsNotEqualTo = 12
    }
}
