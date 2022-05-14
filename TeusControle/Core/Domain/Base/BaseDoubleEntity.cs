﻿using System;

namespace Core.Domain.Base
{
    public class BaseDoubleEntity
    {
        public int Id { get; set; }
        
        public int Id2 { get; set; }
        
        public bool Active { get; set; }

        public bool Deleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? LastChange { get; set; }

        public int CreatedBy { get; set; }

        // public Users CreatedByUser { get; set; }
    }
}
