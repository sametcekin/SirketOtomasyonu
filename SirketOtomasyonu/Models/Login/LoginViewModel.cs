using System.ComponentModel.DataAnnotations;

namespace SirketOtomasyonu.Models.Login
{
    public class LoginViewModel
    {
        [Required]
        public string KullaniciAdi { get; set; }
        [Required]
        public string Sifre { get; set; }
    }
}
