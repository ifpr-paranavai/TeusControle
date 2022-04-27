using TeusControle.Domain.Models.Enums;

namespace TeusControle.Domain.Models.Dtos
{
    /// <summary>
    /// Dto para cadastrar uma nova entrada de produto
    /// </summary>
    public class CreateEntryModel
    {
        /// <summary>
        /// Descrição
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// Dto para atualizar uma entrada de produto
    /// </summary>
    public class UpdateEntryModel : CreateEntryModel
    {
        /// <summary>
        /// Identificador
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Está ativo?
        /// </summary>
        public bool Active { get; set; }
    }

    /// <summary>
    /// Dto com informações da entrada de produto
    /// </summary>
    public class EntryModel : UpdateEntryModel
    {
        /// <summary>
        /// Situação da entrada
        /// </summary>
        public EntriesStatusEnum Status { get; set; }
    }
}
