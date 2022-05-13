namespace Core.Shared.Models.Request
{
    public class LoginRequest
    {
        /// <summary>
        /// E-mail do usuário
        /// </summary>
        /// <example>johndoe@email.com</example>
        public string Email { get; set; }

        /// <summary>
        /// Senha do usuário
        /// </summary>
        /// <example>minhaSenha123</example>
        public string Password { get; set; }

    }
}
