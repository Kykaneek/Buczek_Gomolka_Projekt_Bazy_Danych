﻿using System;
using System.Collections.Generic;

namespace Bazydanych.Models
{
    public partial class Loading
    {
        public Loading()
        {
            PlannedTraces = new HashSet<PlannedTrace>();
            UnLoadings = new HashSet<UnLoading>();
        }

        public int Id { get; set; }
        public int TraceId { get; set; }
        public int CarId { get; set; }
        public DateTime? Pickupdate { get; set; }
        public TimeSpan? TimeToLoading { get; set; }

        public virtual Car Car { get; set; } = null!;
        public virtual Trace Trace { get; set; } = null!;
        public virtual ICollection<PlannedTrace> PlannedTraces { get; set; }
        public virtual ICollection<UnLoading> UnLoadings { get; set; }
    }
}
