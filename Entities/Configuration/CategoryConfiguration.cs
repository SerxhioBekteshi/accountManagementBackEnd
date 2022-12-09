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
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {

        public void Configure (EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
               new Category
               {
                   Id = 1,
                   Code = "PC",
                   Description = "PERSHKRIM TANI KOMPJUTERI ",
                   DateCreated = DateTime.Now,
               },
               new Category
               {
                   Id = 2,
                   Code = "CD",
                   Description = "PERSHKRIM TANI DISK ",
                   DateCreated = DateTime.Now,
               }
           );    
        }
    }
}
