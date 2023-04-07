using System;
using System.Collections.Generic;

namespace Bazydanych.Models
{
    public partial class Role
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? UserRole { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
