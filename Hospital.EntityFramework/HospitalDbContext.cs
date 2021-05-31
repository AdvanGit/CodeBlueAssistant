using Hospital.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Hospital.EntityFramework
{
    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Belay> Belays { get; set; }
        public DbSet<DepartmentTitle> DepartmentTitles { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Change> Changes { get; set; }
        public DbSet<DiagnosisClass> DiagnosisClasses { get; set; }
        public DbSet<DiagnosisGroup> DiagnosisGroups { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<MedCard> MedCards { get; set; }
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

        public DbSet<PhysioTherapyFactor> PhysioTherapyFactors { get; set; }
        public DbSet<PhysTherFactGroup> PhysTherFactGroups { get; set; }
        public DbSet<PhysTherMethod> PhysTherMethods { get; set; }
        public DbSet<PhysTherMethodGroup> PhysTherMethodGroups { get; set; }
        public DbSet<PhysioTherapyData> PhysioTherapyDatas { get; set; }

        public DbSet<SurgeryOperation> SurgeryOperations { get; set; }
        public DbSet<SurgeryGroup> SurgeryGroups { get; set; }
        public DbSet<SurgeryTherapyData> SurgeryTherapyDatas { get; set; }

        public DbSet<TestTemplate> TestTemplates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PatientConfig());
            modelBuilder.ApplyConfiguration(new StaffConfig());
            modelBuilder.ApplyConfiguration(new EntryConfig());
            modelBuilder.ApplyConfiguration(new MedCardConfig());
            //modelBuilder.Ignore<User>();
            modelBuilder.Ignore<Adress>();
            modelBuilder.Ignore<DomainObject>();

            { //examples fluentApi

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

        public EntityEntry UpdateWithoutTracking(object entity)
        {
            ChangeTracker.TrackGraph(
                entity, node =>
                {
                    if (node.Entry.Entity == entity)
                    {
                        var propertyEntry = node.Entry.Property("Id");
                        var keyValue = (int)propertyEntry.CurrentValue;
                        if (keyValue == 0)
                        {
                            node.Entry.State = EntityState.Added;
                        }
                        else if (keyValue < 0)
                        {
                            propertyEntry.CurrentValue = -keyValue;
                            node.Entry.State = EntityState.Deleted;
                        }
                        else
                        {
                            node.Entry.State = EntityState.Added;
                            node.Entry.State = EntityState.Modified;
                        }
                    }
                });
            return Entry(entity);
        }
        public IList<object> UpdateRangeWithoutTracking(IList<object> entities)
        {
            foreach (object entity in entities)
            {
                ChangeTracker.TrackGraph(entity, node =>
                {
                    if (node.Entry.Entity == entity)
                    {
                        var propertyEntry = node.Entry.Property("Id");
                        var keyValue = (int)propertyEntry.CurrentValue;
                        if (keyValue == 0)
                        {
                            node.Entry.State = EntityState.Added;
                        }
                        else if (keyValue < 0)
                        {
                            propertyEntry.CurrentValue = -keyValue;
                            node.Entry.State = EntityState.Deleted;
                        }
                        else
                        {
                            node.Entry.State = EntityState.Added;
                            node.Entry.State = EntityState.Modified;
                        }
                    }
                });
            }
            return entities;
        }

        private class StaffConfig : IEntityTypeConfiguration<Staff>
        {
            public void Configure(EntityTypeBuilder<Staff> builder)
            {
                builder.Property(s => s.IsEnabled).HasDefaultValue(true);
                builder.Property(s => s.LastName).IsRequired();
                builder.Property(s => s.FirstName).IsRequired();
                //builder.Property(s => s.CreateDate).HasDefaultValueSql("NOW()");
                builder.Property(s => s._Adress).HasColumnName("Adress");
                builder.HasOne(s => s.Department).WithMany(d => d.Staffs);

            }
        }
        private class PatientConfig : IEntityTypeConfiguration<Patient>
        {
            public void Configure(EntityTypeBuilder<Patient> builder)
            {
                //builder.Property(s => s.CreateDate).HasDefaultValueSql("NOW()");
                builder.Property(s => s._Adress).HasColumnName("Adress");
                builder.Property(s => s.LastName).IsRequired();
                builder.Property(s => s.FirstName).IsRequired();
                //builder.Ignore(s => s.Adress);
            }
        }
        private class EntryConfig : IEntityTypeConfiguration<Entry>
        {
            public void Configure(EntityTypeBuilder<Entry> builder)
            {
                builder.HasOne(e => e.Registrator).WithMany(s => s.Registrators).OnDelete(DeleteBehavior.NoAction);
                builder.HasOne(e => e.DoctorDestination).WithMany(s => s.DoctorDestinations).OnDelete(DeleteBehavior.NoAction);
                //builder.Property(e => e.CreateDateTime).HasDefaultValueSql("NOW()");
            }
        }
        private class MedCardConfig : IEntityTypeConfiguration<MedCard>
        {
            public void Configure(EntityTypeBuilder<MedCard> builder)
            {
                //builder.Property(p => p.DiagnosisId).IsRequired();
                //builder.Property(p => p.Conclusion).IsRequired();
            }
        }
    }
}

