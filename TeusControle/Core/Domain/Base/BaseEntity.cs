using System;

namespace Core.Domain.Base
{
    public class BaseEntity : ICloneable
    {
        public int Id { get; set; }

        public bool Active { get; set; }

        public bool Deleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? LastChange { get; set; }

        public int? CreatedBy { get; set; }

        public virtual User CreatedByUser { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
