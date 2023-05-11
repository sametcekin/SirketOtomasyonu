namespace SirketOtomasyonu.Data.Entities
{
    public class Iletisim
    {
        public int Id { get; set; }
        public string AdiSoyadi { get; set; }
        public string Email { get; set; }
        public string Baslik { get; set; }
        public string Mesaj { get; set; }
        public DateTime Tarih { get; set; }
    }
}
