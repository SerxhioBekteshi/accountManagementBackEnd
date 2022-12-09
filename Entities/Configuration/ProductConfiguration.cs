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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData
                (
                    new Product
                    {
                        Id = 1,
                        Name = "Sam Raiden",
                        ShortDescription = "HELLO WORLD",
                        LongDescription = "hello world means hello world",
                        Price = 100,
                        image = null,
                        CategoryId = 1,
                        DateCreated = DateTime.Now,
                        DateModified = null,
                    },
                    new Product
                    {
                        Id = 2,
                        Name = "Andrea Mishtaku",
                        ShortDescription = "HELLO Spring",
                        LongDescription = "hello Spring means hello Spring",
                        Price = 100,
                        image = null,
                        CategoryId = 1,
                        DateCreated = DateTime.Now,
                        DateModified = null,
                    },
                    new Product
                    {
                        Id = 3,
                        Name = "Serxhio Bekteshi",
                        ShortDescription = "HELLO .NET",
                        LongDescription = "hello .NET means hello .NET",
                        Price = 100,
                        image = null,
                        CategoryId = 2,
                        DateCreated = DateTime.Now,
                        DateModified = null,
                    }
                );
        }
    }
}
