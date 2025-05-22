using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Models;

namespace PersonelTakipSistemi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Personel> Personeller { get; set; }
        public DbSet<Departman> Departmanlar { get; set; }
        public DbSet<Pozisyon> Pozisyonlar { get; set; }
        public DbSet<Izin> Izinler { get; set; }
        public DbSet<Mesai> Mesailer { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Personel - Departman ilişkisi
            modelBuilder.Entity<Personel>()
                .HasOne(p => p.Departman)
                .WithMany(d => d.Personeller)
                .HasForeignKey(p => p.DepartmanId)
                .OnDelete(DeleteBehavior.Restrict);

            // Personel - Pozisyon ilişkisi
            modelBuilder.Entity<Personel>()
                .HasOne(p => p.Pozisyon)
                .WithMany(poz => poz.Personeller)
                .HasForeignKey(p => p.PozisyonId)
                .OnDelete(DeleteBehavior.Restrict);

            // Personel - İzin ilişkisi
            modelBuilder.Entity<Izin>()
                .HasOne(i => i.Personel)
                .WithMany(p => p.Izinler)
                .HasForeignKey(i => i.PersonelId)
                .OnDelete(DeleteBehavior.Cascade);

            // Personel - Mesai ilişkisi
            modelBuilder.Entity<Mesai>()
                .HasOne(m => m.Personel)
                .WithMany(p => p.Mesailer)
                .HasForeignKey(m => m.PersonelId)
                .OnDelete(DeleteBehavior.Cascade);

            // Personel Maas hassasiyeti
            modelBuilder.Entity<Personel>()
                .Property(p => p.Maas)
                .HasPrecision(18, 2);

            // Varsayılan admin kullanıcısı
            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    Id = 1,
                    Username = "admin",
                    Password = "admin123", // Gerçek uygulamada hash'lenmiş şifre kullanılmalı
                    FullName = "Sistem Yöneticisi",
                    Email = "admin@example.com"
                }
            );
        }
    }
} 