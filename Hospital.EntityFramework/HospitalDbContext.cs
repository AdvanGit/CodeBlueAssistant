using Hospital.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.EntityFramework
{
    public class HospitalDbContext : DbContext
    {
        //public HospitalDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Belay> Belays { get; set; }
        public DbSet<DepartmentTitle> DepartmentTitles { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Change> Changes { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestData> TestDatas { get; set; }
        public DbSet<Proc> Procedures { get; set; }
        public DbSet<ProcAsset> ProcAssets { get; set; }
        public DbSet<ProcOption> ProcOptions { get; set; }
        public DbSet<Presence> Presences { get; set; }
        public DbSet<Entry> Entries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PatientConfig());
            modelBuilder.ApplyConfiguration(new StaffConfig());
            modelBuilder.ApplyConfiguration(new DepartmentConfig());
            modelBuilder.ApplyConfiguration(new EntryConfig());
            modelBuilder.ApplyConfiguration(new PresenceConfig());
            //modelBuilder.Ignore<User>();
            modelBuilder.Ignore<Adress>();
            modelBuilder.Ignore<ModelBase>();

            {
                //modelBuilder.Entity<Country>();   //Fluent API include  
                //modelBuilder.Entity<>().Ignore(b => b.Rate);
                //modelBuilder.Entity<User>().ToTable("People");
                //modelBuilder.Entity<User>().Property(u=>u.Id).HasColumnName("user_id"); 
                //modelBuilder.Entity<User>().HasIndex(u => u.Passport).IsUnique();
                //modelBuilder.Entity<User>().HasKey(u => u.Ident);
                //modelBuilder.Entity<User>().HasKey(u => new { u.PassportSeria, u.PassportNumber});        составной ключ
                //modelBuilder.Entity<User>().HasAlternateKey(u => u.Passport);
                //modelBuilder.Entity<User>().HasAlternateKey(u => new { u.Passport, u.PhoneNumber });
                //modelBuilder.Entity<User>().HasIndex(u => new { u.Passport, u.PhoneNumber });  переопределение индексации


                // [DatabaseGenerated(DatabaseGeneratedOption.None)] autogen off modelBuilder.Entity<User>().Property(b => b.Id).ValueGeneratedNever();
                // [DatabaseGenerated(DatabaseGeneratedOption.Identity)] autogen on
                //modelBuilder.Entity<User>().Property(u => u.Age).HasDefaultValue(18); override default value
                //modelBuilder.Entity<User>().Property(u => u.CreatedAt).HasDefaultValueSql("GETDATE()"); get actual date func
                //modelBuilder.Entity<User>().Property(u => u.Name).HasComputedColumnSql("[FirstName] + ' ' + [LastName]");
                //modelBuilder.Entity<User>().Property(b => b.Name).IsRequired();    [Required] ann
                //modelBuilder.Entity<User>().Property(u=>u.Name).HasColumnType("varchar(200)");
                //modelBuilder.Entity<User>().HasOne(p => p.Company).WithMany(t => t.Users).HasForeignKey(p => p.CompanyInfoKey);   HasOne / HasMany / WithOne / WithMany    [ForeignKey("CompanyInfoKey")]
                //modelBuilder.Entity<User>().HasOne(p => p.Company).WithMany(t => t.Users).HasForeignKey(p => p.CompanyName).HasPrincipalKey(t => t.Name)
                // modelBuilder.Entity<User>().HasOne(p => p.Company).WithMany(t => t.Users).OnDelete(DeleteBehavior.Cascade);  SetNull: Restrict:

            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=HospitalDB;Trusted_Connection=True;");
        }

        private class StaffConfig : IEntityTypeConfiguration<Staff>
        {
            public void Configure(EntityTypeBuilder<Staff> builder)
            {
                builder.Property(s => s.IsEnabled).HasDefaultValue(true);
                builder.Property(s => s.LastName).IsRequired();
                builder.Property(s => s.FirstName).IsRequired();
                builder.Property(s => s.CreateDate).HasDefaultValueSql("GETDATE()");
                builder.Property(s => s._Adress).HasColumnName("Adress");
                builder.HasOne(s => s.Department).WithMany(d => d.Staffs);
                //builder.Ignore(s => s.Adress);

            }
        }
        private class PatientConfig : IEntityTypeConfiguration<Patient>
        {
            public void Configure(EntityTypeBuilder<Patient> builder)
            {
                builder.Property(s => s.CreateDate).HasDefaultValueSql("GETDATE()");
                builder.Property(s => s._Adress).HasColumnName("Adress");
                builder.Property(s => s.LastName).IsRequired();
                builder.Property(s => s.FirstName).IsRequired();
                //builder.Ignore(s => s.Adress);
            }
        }
        private class DepartmentConfig : IEntityTypeConfiguration<Department>
        {
            public void Configure(EntityTypeBuilder<Department> builder)
            {
            }
        }

        private class EntryConfig : IEntityTypeConfiguration<Entry>
        {
            public void Configure(EntityTypeBuilder<Entry> builder)
            {
                builder.HasOne(e => e.Origin).WithOne(p => p.EntryOut).HasForeignKey<Presence>(p => p.EntryOutId).OnDelete(DeleteBehavior.NoAction);
                builder.HasOne(e => e.Resume).WithOne(p => p.EntryIn).HasForeignKey<Presence>(p => p.EntryInId).OnDelete(DeleteBehavior.NoAction);
            }
        }

        private class PresenceConfig : IEntityTypeConfiguration<Presence>
        {
            public void Configure(EntityTypeBuilder<Presence> builder)
            {
                builder.HasOne(p => p.EntryOut).WithOne(e => e.Origin).HasPrincipalKey<Entry>(e => e.OriginId).OnDelete(DeleteBehavior.NoAction);
                builder.HasOne(p => p.EntryIn).WithOne(e => e.Resume).HasPrincipalKey<Entry>(e => e.ResumeId).OnDelete(DeleteBehavior.NoAction);
                builder.HasMany(p => p.TestDatas).WithOne(t => t.Presence);
            }
        }

        private class TestDataConfig : IEntityTypeConfiguration<TestData>
        {
            public void Configure(EntityTypeBuilder<TestData> builder)
            {
                builder.HasOne(t => t.Presence).WithMany(p => p.TestDatas);
            }
        }
    }
}

