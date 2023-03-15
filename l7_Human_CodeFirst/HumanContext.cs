using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace l7_Human_CodeFirst
{
    public class HumanContext : DbContext
    {
        public DbSet<Human> Humans { get; set; }
        public HumanContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["HumansBase"].ConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Human>(builder =>
            {
                builder.ToTable("Humans");

                builder.Property(p => p.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Id");

                builder.Property(p => p.Name)
                    .HasColumnName("Name")
                    .HasMaxLength(100)
                    .IsRequired();

                builder.Property(p => p.Fname)
                    .HasColumnName("Fname")
                    .HasMaxLength(100)
                    .IsRequired();

                builder.Property(p => p.Gender)
                    .HasColumnName("Gender")
                    .HasMaxLength(10)
                    .IsRequired();

                builder.Property(p => p.Age)
                    .HasColumnName("Age")
                    .HasDefaultValue(1);
            });
        }
    }
}
