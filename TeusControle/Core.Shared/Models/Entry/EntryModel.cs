using System;

namespace Core.Shared.Models.Entry
{
    /// <summary>
    /// Objeto de retorno de entrada
    /// </summary>
    public class EntryModel : UpdateEntryModel, ICloneable
    {
        /// <summary>
        /// Data e hora de criação do registro
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Data e hora da última atualização do registro
        /// </summary>
        public DateTime? LastChange { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public EntryModel CloneTyped()
        {
            return (EntryModel)Clone();
        }
    }
}
