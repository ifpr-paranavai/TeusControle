using System;

namespace Core.Shared.Models.User
{
    public class UserPagedModel : ICloneable
    {
        /// <summary>
        /// Identificador
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        public string Name { get; set; }

/*        /// <summary>
        /// Cpf ou Cnpj
        /// </summary>
        public string CpfCnpj { get; set; }*/

        /// <summary>
        /// E-mail
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Data de nascimento
        /// </summary>
        public string BirthDate { get; set; }

        /// <summary>
        /// Tipo do Perfil
        /// </summary>
        public string ProfileType { get; set; }

        /// <summary>
        /// Deve permitir exclusão?
        /// </summary>
        public bool CanBeDeleted { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
