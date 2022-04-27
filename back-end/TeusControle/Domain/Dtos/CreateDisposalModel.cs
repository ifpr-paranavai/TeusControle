using TeusControle.Domain.Models.Enums;

namespace TeusControle.Domain.Models.Dtos
{
    /// <summary>
    /// Dto para cadastrar uma nova saída de produto
    /// </summary>
    public class CreateDisposalModel
    {
        /// <summary>
        /// Tipo de pagamento
        /// </summary>
        public PaymentTypesEnum PaymentType { get; set; }

        /// <summary>
        /// Cpf ou Cnpj
        /// </summary>
        public string CpfCnpj { get; set; }

        /// <summary>
        /// Tipo de saída
        /// </summary>
        public DisposalTypesEnum DisposalTypesEnum { get; set; }
    }

    /// <summary>
    /// Dto para atualizar uma saída de produto
    /// </summary>
    public class UpdateDisposalModel : CreateDisposalModel
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
    /// Dto com informações da saída de produto
    /// </summary>
    public class DisposalModel : UpdateDisposalModel
    {

    }
}
