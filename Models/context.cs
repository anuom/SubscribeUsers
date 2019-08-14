using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SubscribeUsers.Models
{
    public partial class context : DbContext
    {
        public context()
        {
        }

        public context(DbContextOptions<context> options)
            : base(options)
        {
        }

        public virtual DbSet<Subusers> Subusers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-5R345F0;Database=SubscriberDetails;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Subusers>(entity =>
            {
                entity.HasKey(e => e.Uid)
                    .HasName("PK__subusers__C5B196020FBD91B1");

                entity.ToTable("subusers");

                entity.Property(e => e.Uid)
                    .HasColumnName("UID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Uemail)
                    .IsRequired()
                    .HasColumnName("UEmail")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Uname)
                    .HasColumnName("UName")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });
        }
    }
}
