using System;
using System.Collections.Generic;

namespace Bazydanych.Models
{
    public partial class User
    {
        public User()
        {
            PlannedTraces = new HashSet<PlannedTrace>();
            Roles = new HashSet<Role>();
            Userpermissions = new HashSet<Userpermission>();
        }

        public int Id { get; set; }
        public string Login { get; set; } = null!;
        public string Pass { get; set; } = null!;
        public int? Phone { get; set; }
        public string? Licence { get; set; }
        public bool IsDriver { get; set; }
        public bool IsInBase { get; set; }
        public int? PauseTime { get; set; }

        public virtual ICollection<PlannedTrace> PlannedTraces { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Userpermission> Userpermissions { get; set; }
    }
}
