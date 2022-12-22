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
    public class CurrencyConfiguration: IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasData
            (
                new Currency
                {
                    Id = 1,
                    Code = "Bitcoin",
                    Description = "Monedhe virtuale",
                    ExchangeRate = 20203,
                    DateCreated = DateTime.Now,
                    DateModified = null,

                },
                new Currency
                {
                    Id = 2,
                    Code = "Euro",
                    Description = "Monedha e perbashket europiane",
                    ExchangeRate = 120,
                    DateCreated = DateTime.Now,
                    DateModified = null,

                }
            );
        }
    }
}
