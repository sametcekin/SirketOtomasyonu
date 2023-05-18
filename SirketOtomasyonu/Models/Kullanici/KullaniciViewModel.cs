using System.ComponentModel;

namespace SirketOtomasyonu.Models.Kullanici
{
    public class KullaniciViewModel
    {
        public KullaniciViewModel()
        {
            GirisTarihi = DateTime.Now;
        }
        public int Id { get; set; }
        public int YetkiId { get; set; }
        [DisplayName("Kullanıcı Adı")]
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        [DisplayName("Adı")]
        public string Adi { get; set; }
        [DisplayName("Soyadı")]
        public string Soyadi { get; set; }
        public string Email { get; set; }
        [DisplayName("Aktif")]
        public bool IsActive { get; set; }
        [DisplayName("Giris Tarihi")]
        public DateTime GirisTarihi { get; set; }

        public Data.Entities.Yetki Yetki { get; set; }

        public string KullaniciAdiSoyadi { get { return $"{Adi} {Soyadi}"; } }
    }
}
