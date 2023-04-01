using System;
using System.Collections.Generic;

namespace Bazydanych.Models
{
    public partial class Userpermission
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PermissionId { get; set; }

        public virtual Permission Permission { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
