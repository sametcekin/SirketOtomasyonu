using Microsoft.EntityFrameworkCore;
using SirketOtomasyonu.Data.Entities;

namespace SirketOtomasyonu.Data
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=SirketOtomasyonu;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        public DbSet<Birim> Birim { get; set; }
        public DbSet<Duyuru> Duyuru { get; set; }
        public DbSet<Iletisim> Iletisim { get; set; }
        public DbSet<Kullanici> Kullanici { get; set; }
        public DbSet<Personel> Personel { get; set; }
        public DbSet<Sirket> Sirket { get; set; }
        public DbSet<Yetki> Yetki { get; set; }
    }
}
