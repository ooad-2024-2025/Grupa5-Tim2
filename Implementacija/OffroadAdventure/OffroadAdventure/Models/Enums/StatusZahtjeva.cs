using System.ComponentModel.DataAnnotations;

namespace OffroadAdventure.Models.Enums
{
    public enum StatusZahtjeva
    {
        [Display(Name = "Na čekanju")]
        NA_CEKANJU,
        [Display(Name = "Odobren")]
        ODOBREN,
        [Display(Name = "Odbijen")]
        ODBIJEN
    }
}
