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
    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {

        public void Configure(EntityTypeBuilder <BankAccount> builder )
        {
            builder.HasData(
                new BankAccount
                { 
                     Id = 1,
                     Code = "Credins",
                     Name = "Credins Bank",
                     ClientId = 1,
                     Balance = 10000,
                     IsActive = true,
                     DateCreated = DateTime.Now,
                     DateModified = null
                },
                 new BankAccount
                 {
                     Id = 2,
                     Code = "BKT",
                     Name = "BKT Bank",
                     ClientId = 1,
                     Balance = 10000,
                     IsActive = true,
                     DateCreated = DateTime.Now,
                     DateModified = null
                 }
            );
        }
    }
}
