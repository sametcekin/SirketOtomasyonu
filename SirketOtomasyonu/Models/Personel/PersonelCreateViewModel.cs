using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace SirketOtomasyonu.Models.Personel
{
    public class PersonelCreateViewModel
    {
        public int BirimId { get; set; }
        public List<SelectListItem> Birimler { get; set; }

        [DisplayName("Adı")]
        public string Adi { get; set; }
        [DisplayName("Soyadı")]
        public string Soyadi { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public string Email { get; set; }
        public string Adres { get; set; }
        [DisplayName("Maaş")]
        public decimal Maas { get; set; }
        public string Resim { get; set; }
        [DisplayName("Açıklama")]
        public string Aciklama { get; set; }
        [DisplayName("Aktif")]
        public bool IsActive { get; set; }
        public DateTime GirisTarihi { get; set; }
    }
}
