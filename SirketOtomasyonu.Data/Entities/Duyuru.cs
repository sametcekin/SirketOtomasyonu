using System.ComponentModel;

namespace SirketOtomasyonu.Data.Entities
{
    public class Duyuru
    {
        public int Id { get; set; }
        public string Baslik { get; set; }
        public string Icerik { get; set; }
        public string Aciklama { get; set; }
        public DateTime Tarih { get; set; }
    }
}
