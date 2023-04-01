using System;
using System.Collections.Generic;

namespace Bazydanych.Models
{
    public partial class UnLoading
    {
        public int Id { get; set; }
        public TimeSpan? TimeToUnloading { get; set; }
        public int LoadingId { get; set; }

        public virtual Loading Loading { get; set; } = null!;
    }
}
