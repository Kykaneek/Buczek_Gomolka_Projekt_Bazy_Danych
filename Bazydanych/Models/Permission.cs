using System;
using System.Collections.Generic;

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

        public virtual ICollection<Userpermission> Userpermissions { get; set; }
    }
}
