﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Exercises.Register
{
    public class GuineaPig : Animal
    {
        public GuineaPig(int id, string name, string breed, DateTime birthDate, Gender gender)
            : base(id, name, breed, birthDate, gender)
        {
        }

        public override bool RequiresVaccination => false;
    }
}
