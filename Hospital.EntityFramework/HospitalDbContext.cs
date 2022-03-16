using Hospital.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
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
            modelBuilder.Ignore<Adress>();
            modelBuilder.Ignore<DomainObject>();
        }


        /// <summary>
        /// если возникает баг с коллизией сущьностей при апдейте записей, то возможно использовать эти методы
        /// /summary>
        /// <param name="entity">вложенная сущность, которую нужно обновить</param>
        /// <returns></returns>
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
                //builder.Property(s => s.CreateDate).HasDefaultValueSql("NOW()"); //зависит от провайдера, лучше настраивать вручную
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
    }
}

