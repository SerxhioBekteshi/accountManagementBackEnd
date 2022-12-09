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
    public class MenuConfiguration : IEntityTypeConfiguration<ApplicationMenu>
    {

        public void Configure(EntityTypeBuilder<ApplicationMenu> builder)
        {
            builder.HasData(
                new ApplicationMenu
                {
                    Id = 1,
                    Title = "Manager",
                    Route = "/manager",
                    Icon = "fa-regular fa-user",
                    RoleId =  1,

                },
                 new ApplicationMenu
                 {
                     Id = 2,
                     Title = "Employess",
                     Route = "/manager/employees",
                     Icon = "fa-solid fa-briefcase",
                     RoleId = 1,
                 },
                new ApplicationMenu
                {
                    Id = 3,
                    Title = "Admin",
                    Route = "/admin",
                    Icon  = "fa-regular fa-user",
                    RoleId = 2,
                },
                new ApplicationMenu
                {
                    Id = 4,
                    Title = "Companies",
                    Route = "/admin/companies",
                    Icon = "fa-regular fa-building",
                    RoleId = 2,
                },
                new ApplicationMenu
                {
                    Id = 5,
                    Title = "Order Transactions",
                    Route = "/admin/order-transactions",
                    Icon = "fa-solid fa-wallet",
                    RoleId = 2,
                },
                new ApplicationMenu
                {
                    Id = 6,
                    Title = "Product Categories",
                    Route = "/manager/productCategories",
                    Icon = "fa-solid fa-list",
                    RoleId = 1,
                },
                new ApplicationMenu
                {
                    Id = 7,
                    Title = "Products",
                    Route = "/manager/Products",
                    Icon = "fa-solid fa-box",
                    RoleId = 1,
                }
            );
        }
    }
}
