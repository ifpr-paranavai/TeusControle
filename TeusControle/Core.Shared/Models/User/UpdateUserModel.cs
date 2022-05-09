namespace Core.Shared.Models.User
{
    /// <summary>
    /// Objeto para alteração de um usuário
    /// </summary>
    public class UpdateUserModel : CreateUserModel
    {
        /// <summary>
        /// Id do usuário
        /// </summary>
        /// <example>123</example>
        public int Id { get; set; }

        /// <summary>
        /// Status do registro
        /// </summary>
        /// <example>true</example>
        public bool Active { get; set; }
    }
}
