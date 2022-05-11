using Core.Domain.Base;
using Core.Shared.Models.Enums;
using System;
using System.Collections.Generic;

namespace Core.Domain
{
    public class User : BaseEntity
    {
        public string Name { get; set; }

        public string CpfCnpj { get; set; }

        public int DocumentType { get; set; }

        public DateTime? BirthDate { get; set; }

        public string ProfileImage { get; set; }

        public ProfileTypesEnum ProfileType { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public ICollection<User> CreatedUsers { get; set; }

        // public ICollection<ProductEntries> ProductEntries { get; set; }

        // public ICollection<Disposals> Disposals { get; set; }

        // public ICollection<ProductDisposals> ProductDisposals { get; set; }

        // public ICollection<Products> Products { get; set; }

        // public ICollection<Entries> Entries { get; set; }
    }
}
