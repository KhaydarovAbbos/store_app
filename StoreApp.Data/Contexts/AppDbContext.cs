using Microsoft.EntityFrameworkCore;
using StoreApp.Domain.Entities.Products;
using StoreApp.Domain.Entities.Report;
using StoreApp.Domain.Entities.Stores;
using StoreApp.Domain.Entities.Users;

namespace StoreApp.Data.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Store> Stores { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<SubCategory> SubCategories { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<StoreProduct> StoreProducts { get; set; }

        public DbSet<ReceiveReport> ReceiveReports { get; set; }

        public DbSet<Cash> Cashs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("datasource = 127.0.0.1; port=3306; username = root; database=storeapp", new MySqlServerVersion(new Version(8, 0, 11)));
        }

    }
}
