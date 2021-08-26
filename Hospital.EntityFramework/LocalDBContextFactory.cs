using Microsoft.EntityFrameworkCore;

namespace Hospital.EntityFramework
{
    public class LocalDBContextFactory : IDbContextFactory<HospitalDbContext>
    {
        private readonly string _connectionString;

        public LocalDBContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public HospitalDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<HospitalDbContext>();
            optionsBuilder.UseSqlServer(_connectionString, b => b.MigrationsAssembly("Hospital.WPF"));
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);
            optionsBuilder.EnableSensitiveDataLogging(true);
            return new HospitalDbContext(optionsBuilder.Options);
        }
    }
}
