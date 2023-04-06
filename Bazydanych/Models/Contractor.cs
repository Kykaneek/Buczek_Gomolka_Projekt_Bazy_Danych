using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bazydanych.Models
{
    public partial class Contractor
    {
        public Contractor()
        {
            ContractorLocations = new HashSet<ContractorLocation>();
            Traces = new HashSet<Trace>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public long? Nip { get; set; }
        public long? Pesel { get; set; }
        public int? LocationId { get; set; }
        [NotMapped]
        public virtual ICollection<ContractorLocation> ContractorLocations { get; set; }
        [NotMapped]
        public virtual ICollection<Trace> Traces { get; set; }
    }
}
