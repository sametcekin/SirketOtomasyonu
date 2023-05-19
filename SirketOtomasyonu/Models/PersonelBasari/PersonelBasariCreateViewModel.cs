namespace SirketOtomasyonu.Models.PersonelBasari
{
    public class PersonelBasariCreateViewModel
    {
        public PersonelBasariCreateViewModel()
        {
            Tarih = DateTime.Now;
        }
        public int Id { get; set; }

        public int PersonelId { get; set; }

        public string Aciklama { get; set; }

        public DateTime Tarih { get; set; }

        public Data.Entities.Personel Personel { get; set; }
    }
}
