using System;

namespace Core.Domain.Base
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public bool Active { get; set; }

        public bool Deleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? LastChange { get; set; }

        public int CreatedBy { get; set; }

        public User CreatedByUser { get; set; }
    }
}
