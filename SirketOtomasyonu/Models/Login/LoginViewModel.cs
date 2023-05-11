using System.ComponentModel.DataAnnotations;

namespace SirketOtomasyonu.Models.Login
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
