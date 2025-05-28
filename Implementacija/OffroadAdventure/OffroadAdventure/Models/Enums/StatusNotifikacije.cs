using System.ComponentModel.DataAnnotations;

namespace OffroadAdventure.Models.Enums
{
    public enum StatusNotifikacije
    {
        [Display(Name = "Procitana")]
        PROCITANA,
        [Display(Name = "Nepročitana")]
        NEPROCITANA
    }
}