using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Hospital.EntityFramework
{
    public class HospitalDbContextFactory : IDesignTimeDbContextFactory<HospitalDbContext>
    {
        string host = "ec2-54-155-254-112.eu-west-1.compute.amazonaws.com";
        string port = "5432";
        string database = "ddeve4vq2j25ti";
        string userId = "oakqdehpmfxljl";
        string password = "4a1be65e2eb1a1c75aa261d71c94a30d2ee2cda60745a00cf00fd4637463aa34";
        bool ssl = true;

        public HospitalDbContext CreateDbContext(string[] args = null)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HospitalDbContext>();
            optionsBuilder.UseNpgsql($"Host = {host}; Port = {port}; Database = {database}; User Id = {userId}; Password = {password}; Trust Server Certificate = {(ssl ? "true" : "false")} ; SSL Mode = {(ssl ? "Require" : "Disable")}; ");
            //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=HospitalDB;Trusted_Connection=True;");
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);
            optionsBuilder.EnableSensitiveDataLogging(true);
            return new HospitalDbContext(optionsBuilder.Options);
        }
    }
}
