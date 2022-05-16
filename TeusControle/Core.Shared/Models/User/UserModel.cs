using System;

namespace Core.Shared.Models.User
{
    /// <summary>
    /// Objeto de retorno de usuário
    /// </summary>
    public class UserModel : UpdateUserModel, ICloneable
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

        public UserModel CloneTyped()
        {
            return (UserModel)Clone();
        }
    }
}
