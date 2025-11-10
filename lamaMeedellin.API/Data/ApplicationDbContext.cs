using Microsoft.EntityFrameworkCore;
using LAMAMedellin.API.Models;

namespace LAMAMedellin.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Miembro> Miembros { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Índices únicos
            modelBuilder.Entity<Miembro>()
                .HasIndex(m => m.Cedula)
                .IsUnique();

            modelBuilder.Entity<Miembro>()
                .HasIndex(m => m.CorreoElectronico)
                .IsUnique();

            modelBuilder.Entity<Miembro>()
                .HasIndex(m => m.Member)
                .IsUnique();

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Relación Usuario-Miembro
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Miembro)
                .WithMany()
                .HasForeignKey(u => u.MiembroId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}