using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure (EntityTypeBuilder<Employee> builder)
        {
            builder.HasData
                (
                    new Employee
                    {
                        Id = 1,
                        Name = "Sam Raiden",
                        Age = 26,
                        Position = "Software Developer",
                        CompanyId = 1
                    },
                    new Employee
                    {
                        Id = 2,
                        Name = "Jana MclEaf",
                        Age = 24,
                        Position = "Software Developer",
                        CompanyId = 1
                    },
                    new Employee
                    {
                        Id = 3,
                        Name = "kane Miller",
                        Age = 25,
                        Position = "Adm",
                        CompanyId = 2
                    },
                    new Employee
                    {
                        Id = 4,
                        Name = "Caush Cani",
                        Age = 25,
                        Position = "React Developer",
                        CompanyId = 3
                    },
                     new Employee
                     {
                         Id = 5,
                         Name = "Andrea Mishtaku",
                         Age = 25,
                         Position = "React Developer",
                         CompanyId = 3
                     },
                     new Employee
                     {
                         Id = 6,
                         Name = "Serxhio Bekteshi",
                         Age = 25,
                         Position = "React Developer",
                         CompanyId = 4
                     }
                );
        }
    }
}
