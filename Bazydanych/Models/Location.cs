using System;
using System.Collections.Generic;

namespace Bazydanych.Models
{
    public partial class Location
    {
        public Location()
        {
            ContractorLocations = new HashSet<ContractorLocation>();
            TraceFinishLocationNavigations = new HashSet<Trace>();
            TraceStartLocationNavigations = new HashSet<Trace>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? Number { get; set; }

        public virtual ICollection<ContractorLocation> ContractorLocations { get; set; }
        public virtual ICollection<Trace> TraceFinishLocationNavigations { get; set; }
        public virtual ICollection<Trace> TraceStartLocationNavigations { get; set; }
    }
}
