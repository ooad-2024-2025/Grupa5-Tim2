using System.ComponentModel.DataAnnotations;

namespace OffroadAdventure.Models.Enums
{
    public enum StatusPlacanja
    {
        [Display(Name = "Na cekanju")]
        NA_CEKANJU,
        [Display(Name = "Uspjesno placeno")]
        PLACENO,
        [Display(Name = "Odbijeno")]
        ODBIJENO
    }
}
