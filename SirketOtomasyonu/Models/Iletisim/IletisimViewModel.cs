using System.ComponentModel;

namespace SirketOtomasyonu.Models.Iletisim
{
    public class IletisimViewModel
    {
        public int Id { get; set; }
        [DisplayName("Adı Soyadı")]
        public string AdiSoyadi { get; set; }
        public string Email { get; set; }
        [DisplayName("Başlık")]
        public string Baslik { get; set; }
        public string Mesaj { get; set; }
        public DateTime Tarih { get; set; }
    }
}
