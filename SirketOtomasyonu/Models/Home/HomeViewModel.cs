namespace SirketOtomasyonu.Models.Home
{
    public class HomeViewModel
    {
        public int TotalAdminSayisi { get; set; }
        public decimal EnYuksekMaas { get; set; }
        public decimal EnDusukMaas { get; set; }
        public int TotalKullaniciSayisi { get; set; }

        public List<Data.Entities.Personel> PersonselListe { get; set; }

        public List<Data.Entities.Duyuru> DuyuruListe { get; set; }
    }
}
