using System;

namespace TeusControle.Domain.Models.Dtos
{
    /// <summary>
    /// Dto para cadastrar um novo usuário
    /// </summary>
    public class CreateUserModel
    {
        /// <summary>
        /// Nome
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Número do Cpf ou Cnpj
        /// </summary>
        public string CpfCnpj { get; set; }

        /// <summary>
        /// Tipo do documento
        /// </summary>
        public int DocumentType { get; set; }

        /// <summary>
        /// Data de nascimento
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Imagem de perfil
        /// </summary>
        public string ProfileImage { get; set; }

        /// <summary>
        /// Tipo do perfil
        /// </summary>
        public int ProfileType { get; set; }

        /// <summary>
        /// Nome do usuário
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Senha
        /// </summary>
        public string Password { get; set; }
        
        /// <summary>
        /// E-mail
        /// </summary>
        public string Email { get; set; }
    }

    /// <summary>
    /// Dto para atualizar um usuário
    /// </summary>
    public class UpdateUserModel : CreateUserModel
    {
        /// <summary>
        /// Identificador
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Está ativo?
        /// </summary>
        public bool Active { get; set; }
    }

    /// <summary>
    /// Dto com informações do usuário
    /// </summary>
    public class UserModel : UpdateUserModel
    {

    }
}
