using System.ComponentModel;

namespace SirketOtomasyonu.Models.Birim
{
    public class BirimViewModel
    {
        public int Id { get; set; }
        [DisplayName("Adı")]
        public string Adi { get; set; }
        [DisplayName("Açıklama")]
        public string Aciklama { get; set; }
    }
}
