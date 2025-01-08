using Microsoft.EntityFrameworkCore;
using System.Reflection;
using FarmManagement.Domain.Entities;

namespace FarmManagement.Infrastructure.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
        {

        }

        public DbSet<Field> Fields => Set<Field>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            DateTime seedDate = new DateTime(2025, 01, 01); 

            modelBuilder.Entity<Field>().HasData(
                new Field { Id = 1, Name = "Corn Field", Area = 1000, CropName = "Corn", CreatedBy = 1, CreatedDate = seedDate },
                new Field { Id = 2, Name = "Josh’s Dams", Area = 1200, CropName = "Yams", CreatedBy = 1, CreatedDate = seedDate },
                new Field { Id = 3, Name = "eans", Area = 300, CropName = "Red Beans", CreatedBy = 1, CreatedDate = seedDate },
                new Field { Id = 4, Name = "Dav's Wheat", Area = 4000, CropName = "Wheat", CreatedBy = 1, CreatedDate = seedDate });

        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public override int SaveChanges()
        {
            return SaveChangesAsync().GetAwaiter().GetResult();
        }

    }
}
