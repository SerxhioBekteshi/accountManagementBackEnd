﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class EmployeeForCreationAndUpdateDto
    {


        public string Name { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }
    }
}
