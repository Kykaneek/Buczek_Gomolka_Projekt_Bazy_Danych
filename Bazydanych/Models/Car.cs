﻿using System;
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
        public string RegistrationNumber { get; set; } = null!;
        public string Mileage { get; set; } = null!;
        public DateTime BuyDate { get; set; }
        public bool IsTruck { get; set; }
        public int Loadingsize { get; set; }
        public bool IsAvailable { get; set; }
        [NotMapped]
        public virtual ICollection<Loading> Loadings { get; set; }
        [NotMapped]
        public virtual ICollection<PlannedTrace> PlannedTraces { get; set; }
    }
}
