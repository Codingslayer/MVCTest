using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TestMVC.Models
{
    public partial class PruebaMVCContext : DbContext
    {
        public PruebaMVCContext()
        {
        }

        public PruebaMVCContext(DbContextOptions<PruebaMVCContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Tblciudad> Tblciudads { get; set; }
        public virtual DbSet<Tblpersona> Tblpersonas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=ADMIN\\SQLEXPRESS01;Database=PruebaMVC; trusted_Connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Tblciudad>(entity =>
            {
                entity.ToTable("Tblciudad");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tblpersona>(entity =>
            {
                entity.ToTable("Tblpersona");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Edad)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsFixedLength(true);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .IsFixedLength(true);

                entity.HasOne(d => d.IdCiudadNavigation)
                    .WithMany(p => p.Tblpersonas)
                    .HasForeignKey(d => d.IdCiudad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tblpersona_Tblciudad");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
