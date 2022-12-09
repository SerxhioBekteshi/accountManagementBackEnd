﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public DateTime BirthDate { get; set; }
        public int? RoleId { get; set; }
        public string? Role { get; set; }
        public DateTime? DateCreated { get; set; }
    
        //public string TokenHas { get; set; }

    }
}
