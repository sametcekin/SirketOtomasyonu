using System.ComponentModel;

namespace SirketOtomasyonu.Data.Entities
{
    public class Birim
    {
        public int Id { get; set; }
        [DisplayName("Adı")]
        public string Adi { get; set; }
        [DisplayName("Açıklama")]
        public string Aciklama { get; set; }
    }
}
