using Microsoft.AspNetCore.Mvc.Rendering;

namespace SirketOtomasyonu.Models.PersonelBasari
{
    public class PersonelBasariViewModel
    {
        public PersonelBasariViewModel()
        {
            PersonelBasariListe = new();
            PersonelListe = new();
        }
        public List<Data.Entities.PersonelBasari> PersonelBasariListe { get; set; }

        public int PersonelId { get; set; }

        public List<SelectListItem> PersonelListe { get; set; }
    }
}
