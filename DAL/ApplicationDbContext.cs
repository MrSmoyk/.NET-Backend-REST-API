using Domain.Entitys;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Operation> Operations { get; set; }

        public DbSet<OperationType> OperationTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Operation>().ToTable("Operations");
            modelBuilder.Entity<OperationType>().ToTable("OperationTypes");

            modelBuilder.Entity<OperationType>()
                .HasIndex(operationType => operationType.Name)
                .IsUnique(true);

            modelBuilder.Entity<OperationType>().HasData
                (
                   new OperationType { Name = "Incomes", Description = "string12", IsIncome = true, Id = Guid.Parse("6a6da227-6d4c-42eb-9eb6-af3d7ddfca45") },
                   new OperationType { Name = "Expenses", Description = "string1", IsIncome = false, Id = Guid.Parse("61047354-b6fc-42c3-94c3-3b4f24caecf0") },
                   new OperationType { Name = "Incomes2", Description = "string123", IsIncome = true, Id = Guid.Parse("6de4828d-5003-47ec-b5e3-30e081c48ef2") }
                );

            modelBuilder.Entity<Operation>().HasData
                (
                   new Operation { Name = "Income", Amount = 2147, Created = DateTime.ParseExact("2022-12-20", "yyyy-MM-dd", null), Description = "desc", TypeId = Guid.Parse("6a6da227-6d4c-42eb-9eb6-af3d7ddfca45"), Id = Guid.Parse("ef1db03b-a3b1-4144-b0b3-acb6fc10ba2c") },
                   new Operation { Name = "Expense", Amount = 2147, Created = DateTime.ParseExact("2022-12-20", "yyyy-MM-dd", null), Description = "desc", TypeId = Guid.Parse("61047354-b6fc-42c3-94c3-3b4f24caecf0"), Id = Guid.Parse("3cdebedb-b2ca-45c5-b50e-bca0a2a7939d") },
                   new Operation { Name = "Income", Amount = 2147, Created = DateTime.ParseExact("2022-12-20", "yyyy-MM-dd", null), Description = "desc", TypeId = Guid.Parse("6a6da227-6d4c-42eb-9eb6-af3d7ddfca45"), Id = Guid.Parse("4fec9b3e-ab26-432e-9bcc-d7b68d9f3272") },
                   new Operation { Name = "Income", Amount = 2147, Created = DateTime.ParseExact("2022-12-21", "yyyy-MM-dd", null), Description = "desc", TypeId = Guid.Parse("6de4828d-5003-47ec-b5e3-30e081c48ef2"), Id = Guid.Parse("8b09ced8-7710-4742-b830-93885a7f0f95") }
                );
        }

        protected override void OnConfiguring
       (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging().UseInMemoryDatabase(databaseName: "Db");
        }
    }
}
