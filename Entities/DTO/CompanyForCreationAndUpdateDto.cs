﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class CompanyForCreationAndUpdateDto
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public List<int?>? CategoryIds { get; set; }
        //public List<int?>? ProductIds { get; set; }


    }
}
