namespace SirketOtomasyonu.Data.Entities
{
    public class PersonelBasari
    {
        public int Id { get; set; }

        public int PersonelId { get; set; }

        public string Aciklama { get; set; }

        public DateTime Tarih { get; set; }

        public Personel Personel { get; set; }
    }
}
