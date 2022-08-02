using Core.Shared.Models.Enums;
using System;

namespace Core.Shared.Models.User
{
    /// <summary>
    /// Objeto para criação de um novo usuário
    /// </summary>
    public class CreateUserModel
    {
        /// <summary>
        /// Nome do usuário
        /// </summary>
        /// <example>John Doe</example>
        public string Name { get; set; }

        /*/// <summary>
        /// Documento do usuário CPF ou CNPJ
        /// </summary>
        /// <example>69909465043</example>
        public string CpfCnpj { get; set; }

        /// <summary>
        /// Tipo do documento do usuário
        /// </summary>
        /// <example>1</example>
        public int DocumentType { get; set; }*/

        /// <summary>
        /// Data de nascimento do usuário
        /// </summary>
        /// <example>2001-11-27</example>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Imagem de perfil do usuário
        /// </summary>
        /// <example>https://ciclovivo.com.br/wp-content/uploads/2018/10/iStock-536613027-696x464.jpg</example>
        public string ProfileImage { get; set; }
        
        /// <summary>
        /// Tipo de Usuário
        /// </summary>
        public ProfileTypesEnum ProfileType { get; set; }

        /// <summary>
        /// Senha do usuário
        /// </summary>
        /// <example>minhaSenha123</example>
        public string Password { get; set; }

        /// <summary>
        /// E-mail do usuário
        /// </summary>
        /// <example>johndoe@email.com</example>
        public string Email { get; set; }
    }
}
