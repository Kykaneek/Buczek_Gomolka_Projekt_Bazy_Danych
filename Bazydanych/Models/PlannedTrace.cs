using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bazydanych.Models
{
    public partial class PlannedTrace
    {
        public int Id { get; set; }
        public int TraceId { get; set; }
        public int UserId { get; set; }
        public int CarId { get; set; }
        public int? NextPlannedTraceId { get; set; }
        public int? LoadingId { get; set; }
        [NotMapped]
        public virtual Car Car { get; set; } = null!;
        [NotMapped]
        public virtual Loading? Loading { get; set; }
        [NotMapped]
        public virtual Trace Trace { get; set; } = null!;
        [NotMapped]
        public virtual User User { get; set; } = null!;
    }
}
