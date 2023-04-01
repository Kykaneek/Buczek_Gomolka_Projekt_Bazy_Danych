using System;
using System.Collections.Generic;

namespace Bazydanych.Models
{
    public partial class Role
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool Driver { get; set; }
        public bool Planner { get; set; }
        public bool TmsAdmin { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
