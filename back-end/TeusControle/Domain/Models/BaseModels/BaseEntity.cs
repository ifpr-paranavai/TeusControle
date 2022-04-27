using System;

namespace TeusControle.Domain.Models.CommonModels
{
    /// <summary>
    /// Classe genérica para entidades
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Identificador - ou chave primaria composta
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Está ativo?
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Está deletado?
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Data de criação do registro
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Data da última atualização do registro
        /// </summary>
        public DateTime? LastChange { get; set; }

        /// <summary>
        /// Identificador do usuário que criou o registro
        /// </summary>
        public long CreatedBy { get; set; }

        /// <summary>
        /// Usuário que criou o registro
        /// </summary>
        public Users CreatedByUser { get; set; }
    }
}
