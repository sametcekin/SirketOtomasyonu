using System.ComponentModel;

namespace SirketOtomasyonu.Models.Yetki
{
    public class YetkiViewModel
    {
        public int Id { get; set; }
        [DisplayName("Adı")]
        public string Adi { get; set; }
    }
}
