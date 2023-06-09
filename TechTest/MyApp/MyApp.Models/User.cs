﻿using MyApp.Models.Base;
using System;
using System.Collections.Generic;

namespace MyApp.Models
{
    public class User : ModelBase
    {
        public string Forename { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public ICollection<Activity> Activities { get; set; }
    }
}