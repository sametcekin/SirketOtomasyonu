using Microsoft.Build.Framework;
using System.ComponentModel;

namespace SirketOtomasyonu.Models.Birim
{
    public class BirimViewModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Adı")]
        public string Adi { get; set; }
        [Required]
        [DisplayName("Açıklama")]
        public string Aciklama { get; set; }
    }
}
