using System.ComponentModel.DataAnnotations;

namespace SirketOtomasyonu.Models.Account
{
    public class LoginViewModel
    {
        [Required]
        public string KullaniciAdi { get; set; }
        [Required]
        public string Sifre { get; set; }

        public bool BeniHatirla { get; set; }
    }
}
