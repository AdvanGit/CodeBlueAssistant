using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WpfApp1.Model;

namespace WpfApp1.Context
{
    class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Staff>  Staffs { get; set; }

        public UserContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
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

            modelBuilder.Ignore<Department>();   //Fluent API exclude      [NotMapped] annotation
            modelBuilder.Ignore<Belay>();
            modelBuilder.Entity<User>().Property(u => u.CreateDate).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Staff>().Property(s => s.IsEnabled).HasDefaultValue(true);

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
}
