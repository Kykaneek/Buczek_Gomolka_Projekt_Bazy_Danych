using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
        [NotMapped]
        public int contractorID { get; set; } 
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? Number { get; set; }
        [NotMapped]
        public virtual ICollection<ContractorLocation> ContractorLocations { get; set; }
        [NotMapped]
        public virtual ICollection<Trace> TraceFinishLocationNavigations { get; set; }
        [NotMapped]
        public virtual ICollection<Trace> TraceStartLocationNavigations { get; set; }
    }
}
