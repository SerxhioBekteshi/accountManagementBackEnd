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
    public class BankConfiguration : IEntityTypeConfiguration<Bank>
    {

        public void Configure(EntityTypeBuilder <Bank> builder )
        {
            builder.HasData(
                new Bank
                { 
                     Id = 1,
                     Code = "Credins",
                     Name = "Credins Bank",
                     DateCreated = DateTime.Now,
                     DateModified = null
                },
                 new Bank
                 {
                     Id = 2,
                     Code = "BKT",
                     Name = "BKT Bank",
                     DateCreated = DateTime.Now,
                     DateModified = null
                 }
            );
        }
    }
}
