using System;
using System.Collections.Generic;

namespace Bazydanych.Models
{
    public partial class ContractorLocation
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public int ContractorId { get; set; }

        public virtual Contractor Contractor { get; set; } = null!;
        public virtual Location Location { get; set; } = null!;
    }
}
