using System;
using System.Collections.Generic;

namespace Bazydanych.Models
{
    public partial class Trace
    {
        public Trace()
        {
            Loadings = new HashSet<Loading>();
            PlannedTraces = new HashSet<PlannedTrace>();
        }

        public int Id { get; set; }
        public int ContractorId { get; set; }
        public int StartLocation { get; set; }
        public int FinishLocation { get; set; }
        public int Distance { get; set; }
        public TimeSpan TravelTime { get; set; }

        public virtual Contractor Contractor { get; set; } = null!;
        public virtual Location FinishLocationNavigation { get; set; } = null!;
        public virtual Location StartLocationNavigation { get; set; } = null!;
        public virtual ICollection<Loading> Loadings { get; set; }
        public virtual ICollection<PlannedTrace> PlannedTraces { get; set; }
    }
}
