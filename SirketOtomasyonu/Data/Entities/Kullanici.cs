namespace SirketOtomasyonu.Data.Entities
{
    public class Kullanici
    {
        public int Id { get; set; }
        public int YetkiId { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string Email { get; set; }
        public string IsActive { get; set; }
        public DateTime GirisTarihi { get; set; }
    }
}
