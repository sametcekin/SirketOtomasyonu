namespace SirketOtomasyonu.Data.Entities
{
    public class Personel
    {
        public int Id { get; set; }
        public int BirimId { get; set; }
        public Birim Birim { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public string Email { get; set; }
        public string Adres { get; set; }
        public decimal Maas { get; set; }
        public string Resim { get; set; }
        public string Aciklama { get; set; }
        public bool IsActive { get; set; }
        public DateTime GirisTarihi { get; set; }

        public string PersonelAdiSoyadi { get { return $"{Adi} {Soyadi}"; } }
    }
}
