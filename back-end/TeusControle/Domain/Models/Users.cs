using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TeusControle.Domain.Models.CommonModels;
using TeusControle.Domain.Models.Enums;

namespace TeusControle.Domain.Models
{
    public partial class Users : BaseEntity
    {
        public Users()
        {
            CreatorUsers = new HashSet<Users>();
            Products = new HashSet<Products>();
            Entries = new HashSet<Entries>();
            ProductEntries = new HashSet<ProductEntries>();
            Disposals = new HashSet<Disposals>();
        }

        /// <summary>
        /// Nome
        /// </summary>
        [Required(ErrorMessage = "O nome deve ser inserido")]
        [MinLength(3, ErrorMessage = "O nome deve conter no mínimo 3 caracteres")]
        [MaxLength(80, ErrorMessage = "O nome deve conter no máximo 80 caracteres")]
        [RegularExpression(@"^[ a-zA-Z á]*$", ErrorMessage = "O nome deve conter apenas letras.")]
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
        /// Tipo de perfil
        /// </summary>
        public ProfileTypesEnum ProfileType { get; set; }

        /// <summary>
        /// Nome de usuário
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Senha
        /// </summary>
        [Required(ErrorMessage = "A senha deve ser inserida")]
        [MinLength(6, ErrorMessage = "A senha deve conter no mínimo 6 caracteres")]
        [MaxLength(80, ErrorMessage = "A senha deve conter no máximo 80 caracteres")]
        public string Password { get; set; }

        /// <summary>
        /// E-mail
        /// </summary>
        [Required(ErrorMessage = "O email deve ser inserido")]
        [MinLength(10, ErrorMessage = "O email deve conter no mínimo 10 caracteres")]
        [MaxLength(150, ErrorMessage = "O email deve conter no máximo 80 caracteres")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "O Email é inválido, insira outro.")]
        public string Email { get; set; }

        /// <summary>
        /// Produtos
        /// </summary>
        public ICollection<Products> Products { get; set; }

        /// <summary>
        /// Registro para entrada de produtos
        /// </summary>
        public ICollection<Entries> Entries { get; set; }

        /// <summary>
        /// Usuários
        /// </summary>
        public ICollection<Users> CreatorUsers { get; set; }

        /// <summary>
        /// Produtos de uma entrada
        /// </summary>
        public ICollection<ProductEntries> ProductEntries { get; set; }

        /// <summary>
        /// Saídas de produtos
        /// </summary>
        public ICollection<Disposals> Disposals { get; set; }

        /// <summary>
        /// Produtos de uma saída
        /// </summary>
        public ICollection<ProductDisposals> ProductDisposals { get; set; }
    }
}
