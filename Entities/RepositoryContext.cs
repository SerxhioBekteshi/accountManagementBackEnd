using Entities.Configuration;
using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class RepositoryContext : IdentityDbContext<User, Roles, int>
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new MenuConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CurrencyConfiguration());
            modelBuilder.ApplyConfiguration(new BankConfiguration());


        }

        public DbSet<User> Roles { get; set; }
        public DbSet<ApplicationMenu> ApplicationMenu { get; set; }
        public DbSet<Company> Companies { get; set;}
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Currency> Currencies{ get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<BankAccount> BankAccounts{ get; set; }
        public DbSet<BankTransaction> BankTransactions { get; set; }
        public DbSet<ProductSaleList> ProductSaleList { get; set; }
        public DbSet<SaleTransaction> SalesTransaction { get; set; }
        public DbSet<CompanyCategory> CompanyCategory { get; set; }
        public DbSet<CurrencyBank> CurrencyBank { get; set; }

    }
}
