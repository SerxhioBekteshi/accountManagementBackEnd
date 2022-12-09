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
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasData
            (
                new Company
                {
                    Id = 1,
                    Name = "IT_Solutions Ltd",
                    Address = "583 Wall Dr. Gwynn Oak, MD 21207",
                    Country  = "USA",
                    ManagerAccountActivated = "Not Registered",
                    ManagerId = 2,
         
                },
                new Company
                {
                    Id = 2,
                    Name = "Admin_Solutions Ltd",
                    Address = "312 Forest Avenue, BF 923",
                    Country = "USA",
                    ManagerAccountActivated = "Not Registered",
                    ManagerId = null,
                },
                new Company
                {
                    Id = 3,
                    Name = "LOCAL WEB",
                    Address = "312 Forest Avenue, BF 923",
                    Country = "USA",
                    ManagerAccountActivated = "Not Registered",
                    ManagerId = 2,
                },
                new Company
                {
                    Id = 4,
                    Name = "aaaa",
                    Address = "bbbbb",
                    Country = "cccc",
                    ManagerAccountActivated = "Not Registered",
                    ManagerId = 2,
                }
            );
        }
    }
}
