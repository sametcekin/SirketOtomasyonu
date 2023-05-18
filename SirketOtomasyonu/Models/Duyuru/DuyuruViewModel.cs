using System.ComponentModel;

namespace SirketOtomasyonu.Models.Duyuru
{
    public class DuyuruViewModel
    {
        public DuyuruViewModel()
        {
            Tarih = DateTime.Now;
        }
        public int Id { get; set; }
        [DisplayName("Başlık")]
        public string Baslik { get; set; }
        [DisplayName("İçerik")]
        public string Icerik { get; set; }
        [DisplayName("Açıklama")]
        public string Aciklama { get; set; }
        public DateTime Tarih { get; set; }
    }
}
