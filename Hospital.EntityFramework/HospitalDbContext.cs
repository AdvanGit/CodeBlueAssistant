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
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Entry> Entries { get; set; }

        public DbSet<Test> Tests { get; set; }
        public DbSet<TestData> TestDatas { get; set; }
        public DbSet<TestNormalValue> TestNormalValues { get; set; }
        public DbSet<TestType> TestTypes { get; set; }

        public DbSet<Drug> Drugs { get; set; }
        public DbSet<DrugSubGroup> DrugSubGroups { get; set; }
        public DbSet<DrugGroup> DrugGroups { get; set; }
        public DbSet<DrugSubClass> DrugSubClasses { get; set; }
        public DbSet<DrugClass> DrugClasses { get; set; }
        public DbSet<PharmacoTherapyData> PharmacoTherapyDatas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PatientConfig());
            modelBuilder.ApplyConfiguration(new StaffConfig());
            modelBuilder.ApplyConfiguration(new DepartmentConfig());
            modelBuilder.ApplyConfiguration(new EntryConfig());
            modelBuilder.ApplyConfiguration(new VisitConfig());
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
                builder.Property(e => e.PatientId).IsRequired();
                builder.Property(e => e.DestinationId).IsRequired();
                builder.Property(e => e.CreateDateTime).HasDefaultValueSql("GETDATE()");
            }
        }

        private class VisitConfig : IEntityTypeConfiguration<Visit>
        {
            public void Configure(EntityTypeBuilder<Visit> builder)
            {
                //builder.Property(p => p.DiagnosisId).IsRequired();
                //builder.Property(p => p.Conclusion).IsRequired();
            }
        }
    }
}

