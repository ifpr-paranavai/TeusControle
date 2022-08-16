namespace Core.Shared.Models.Entry
{
    /// <summary>
    /// Objeto para alteração de uma entrada
    /// </summary>
    public class UpdateEntryModel : CreateEntryModel
    {
        /// <summary>
        /// Id da entrada
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
