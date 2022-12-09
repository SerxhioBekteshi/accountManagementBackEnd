
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Roles>
    {
        public void Configure(EntityTypeBuilder<Roles> builder)
        {
            builder.HasData(
                new Roles
                {
                    Id = 1,
                    Name = "Manager",
                    NormalizedName = "MANAGER"
                },
                new Roles
                {
                    Id = 2,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new Roles
                {
                    Id = 3,
                    Name = "User",
                    NormalizedName = "USER"
                }
            );
        }

    }
}
