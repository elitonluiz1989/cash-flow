using CashFlow.Entities;
using CashFlow.Infra.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace CashFlow.Infra.Data.Context
{
    public class SQLiteContext : DbContext
    {
        public DbSet<StockItem> StockItem { get; set; }
        public SQLiteContext() : base()
        { }

        public SQLiteContext(DbContextOptions<SQLiteContext> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = $"Data Source={GetDatabasePath()}"; 
            optionsBuilder.UseSqlite(connectionString, options => {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StockItem>(new StockItemMapper().Configure);
            base.OnModelCreating(modelBuilder);
        }

        public async Task InitializeDatabase()
        {
            await Database.EnsureCreatedAsync();
            Database.Migrate();
        }

        private string GetDatabasePath()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "ClashFlow");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return Path.Combine(path, "ClashFlow.db");
        }
    }
}
