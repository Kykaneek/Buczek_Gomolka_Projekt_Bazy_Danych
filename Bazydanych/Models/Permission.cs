using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bazydanych.Models
{
    public partial class Permission
    {
        public Permission()
        {
            Userpermissions = new HashSet<Userpermission>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        [NotMapped]
        public virtual ICollection<Userpermission> Userpermissions { get; set; }
    }
}
