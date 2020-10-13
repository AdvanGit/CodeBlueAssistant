using Hospital.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.EntityFramework
{
    public class HospitalDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Belay> Belays { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new DepartmentConfig());
            modelBuilder.Ignore<Department>();

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

        public class UserConfig : IEntityTypeConfiguration<User>
        {
            public void Configure(EntityTypeBuilder<User> builder)
            {
                builder.Property(u => u.CreateDate).HasDefaultValueSql("GETDATE()");
                builder.Property(u => u.FirstName).IsRequired();
                builder.Property(u => u.LastName).IsRequired();
                //Json serialize
                builder.Property(u => u._Adress).HasColumnName("Adress");
                builder.Ignore(u => u.Adress);
            }
        }
        public class DepartmentConfig : IEntityTypeConfiguration<Department>
        {
            public void Configure(EntityTypeBuilder<Department> builder)
            {

            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=HospitalDB;Trusted_Connection=True;");
        }
    }
}

