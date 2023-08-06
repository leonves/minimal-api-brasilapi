using Microsoft.EntityFrameworkCore;
using BrasilApiIntegration.Data.Mapping;
using BrasilApiIntegration.Data.Entities.Core;
using BrasilApiIntegration.Data.Entities;
using BrasilApiIntegration.Data.Extesions;
using Microsoft.Extensions.Configuration;

namespace BrasilApiIntegration.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Weather> Weathers { get; set; }
        public DbSet<Log> Logs { get; set; }

        public AppDbContext()
        {
            
        }

        public AppDbContext(DbContextOptions<DbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region 'EntitiesConfig'

            modelBuilder.ApplyConfiguration(new LogMap());

            modelBuilder.ApplyConfiguration(new WeatherMap());

            #endregion

            modelBuilder.ApplyGlobalStandards();
            modelBuilder.SeedData();


            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
            .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }



        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            ChangeTracker.Entries().ToList().ForEach(entry =>
            {
                if (entry.Entity is Entity trackableEntity)
                {
                    if (entry.State == EntityState.Added)
                    {
                        trackableEntity.CreatedDate = DateTime.Now;
                    }

                    else if (entry.State == EntityState.Modified)
                        trackableEntity.ModifiedDate = DateTime.Now;
                }
            });
        }
    }
}
