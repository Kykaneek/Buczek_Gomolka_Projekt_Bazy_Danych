using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bazydanych.Models
{
    public partial class Car
    {
        public Car()
        {
            Loadings = new HashSet<Loading>();
            PlannedTraces = new HashSet<PlannedTrace>();
        }

        public int Id { get; set; }
        public int Driver { get; set; }
        public string Registration_Number { get; set; } = null!;
        public string Mileage { get; set; } = null!;
        public DateTime Buy_Date { get; set; }
        public bool IS_truck { get; set; }
        public int loadingsize { get; set; }
        public bool is_available { get; set; }
        [NotMapped]
        public virtual ICollection<Loading> Loadings { get; set; }
        [NotMapped]
        public virtual ICollection<PlannedTrace> PlannedTraces { get; set; }
    }
}
