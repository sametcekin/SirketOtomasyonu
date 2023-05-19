using System.ComponentModel.DataAnnotations;

namespace SirketOtomasyonu.Data.Entities
{
    public class Kullanici
    {
        public int Id { get; set; }
        [Required]
        public int YetkiId { get; set; }
        [Required]
        public string KullaniciAdi { get; set; }
        [Required]
        public string Sifre { get; set; }
        [Required]
        public string Adi { get; set; }
        [Required]
        public string Soyadi { get; set; }
        [Required]
        public string Email { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public DateTime GirisTarihi { get; set; }

        public Yetki Yetki { get; set; }
        public string KullaniciAdiSoyadi { get { return $"{Adi} {Soyadi}"; } }


    }
}
