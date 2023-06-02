﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bazydanych.Models
{
    public partial class Trace
    {
        
        [Key]
        public int Id { get; set; }
        public int Contractor_Id { get; set; }
        public int StartLocation { get; set; }
        public int FinishLocation { get; set; }
        public int Distance { get; set; }
        public TimeSpan Travel_Time { get; set; }


    }
}