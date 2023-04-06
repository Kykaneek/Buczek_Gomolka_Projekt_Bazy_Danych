using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Key]
        public int Id { get; set; }
        public string Login { get; set; } = null!;
        public string Pass { get; set; } = null!;

        public int? Phone { get; set; }
        public string? Licence { get; set; }
        public bool is_driver { get; set; }
        public bool is_in_base { get; set; }
        public int? pause_time { get; set; }
        [NotMapped]
        public virtual ICollection<PlannedTrace> PlannedTraces { get; set; }
        [NotMapped]
        public virtual ICollection<Role> Roles { get; set; }
        [NotMapped]
        public virtual ICollection<Userpermission> Userpermissions { get; set; }
    }
}
