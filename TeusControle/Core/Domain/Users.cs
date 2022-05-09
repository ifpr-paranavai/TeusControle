using Core.Domain.Base;
using System;

namespace Core.Domain
{
    public class Users : BaseEntity
    {
        /*public Users()
        {
            CreatorUsers = new HashSet<Users>();
            Products = new HashSet<Products>();
            Entries = new HashSet<Entries>();
            ProductEntries = new HashSet<ProductEntries>();
            Disposals = new HashSet<Disposals>();
        }*/
        public string Name { get; set; }

        public string CpfCnpj { get; set; }

        public int DocumentType { get; set; }

        public DateTime? BirthDate { get; set; }

        public string ProfileImage { get; set; }

        // public ProfileTypesEnum ProfileType { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        // public ICollection<Products> Products { get; set; }

        // public ICollection<Entries> Entries { get; set; }

        // public ICollection<Users> CreatorUsers { get; set; }

        // public ICollection<ProductEntries> ProductEntries { get; set; }

        // public ICollection<Disposals> Disposals { get; set; }

        // public ICollection<ProductDisposals> ProductDisposals { get; set; }
    }
}
