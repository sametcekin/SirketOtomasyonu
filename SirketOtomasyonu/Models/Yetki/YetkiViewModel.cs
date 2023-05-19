using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SirketOtomasyonu.Models.Yetki
{
    public class YetkiViewModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Adı")]
        public string Adi { get; set; }
    }
}
