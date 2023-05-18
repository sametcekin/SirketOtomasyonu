using System.ComponentModel;

namespace SirketOtomasyonu.Data.Entities
{
    public class Yetki
    {
        public int Id { get; set; }
        [DisplayName("Adı")]
        public string Adi { get; set; }
    }
}
