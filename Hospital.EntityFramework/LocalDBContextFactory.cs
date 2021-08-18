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
            optionsBuilder.UseSqlServer(_connectionString);
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);
            #if DEBUG
            optionsBuilder.EnableSensitiveDataLogging(true);
            #endif
            return new HospitalDbContext(optionsBuilder.Options);
        }
    }
}
