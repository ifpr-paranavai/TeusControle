using Core.Domain.Base;
using Core.Shared.Models.Enums;
using System;
using System.Collections.Generic;

namespace Core.Domain
{
    public class User : BaseEntity
    {
        public User()
        {
            CreatedUsers = new HashSet<User>();
            Products = new HashSet<Product>();
            Entries = new HashSet<Entry>();
            ProductsEntry = new HashSet<ProductEntry>();
            ProductsSale = new HashSet<ProductSale>();
        }

        public string Name { get; set; }

        /*public string CpfCnpj { get; set; }*/

       /* public int DocumentType { get; set; }*/

        public DateTime? BirthDate { get; set; }

        public string ProfileImage { get; set; }

        public ProfileTypesEnum ProfileType { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public ICollection<User> CreatedUsers { get; set; }

        public ICollection<Product> Products { get; set; }
        
        public ICollection<Entry> Entries { get; set; }

        public ICollection<ProductEntry> ProductsEntry { get; set; }

        public override object Clone()
        {
            return MemberwiseClone();
        }

        public User CloneTyped()
        {
            return (User)Clone();
        }

        public ICollection<Sale> Sales { get; set; }

        public ICollection<ProductSale> ProductsSale { get; set; }

        // public ICollection<ProductDisposals> ProductDisposals { get; set; }
    }
}
