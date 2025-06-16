using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OffroadAdventure.Models
{
    public class Vozilo
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "Model je obavezan.")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9\s]*$", ErrorMessage = "Model mora počinjati slovom i može sadržavati samo slova, brojeve i razmake.")]
        public string model { get; set; }
        [Required(ErrorMessage = "Tip je obavezan.")]
        [RegularExpression("^(ATV|Buggy|SUV|Motor)$", ErrorMessage = "Dozvoljeni tipovi su: ATV, Buggy, SUV, Motor.")]
        public string tip { get; set; }
        [Required(ErrorMessage = "Morate unijeti cijenu")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "Cijena mora biti pozitivan broj.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Cijena mora biti cijeli broj.")]
        public double cijenaPoDanu { get; set; }
        public Boolean dostupno { get; set; }
        [Url(ErrorMessage = "Unesite ispravan URL slike.")]
        public string slikaURL { get; set; }
        
        public Vozilo() { }
    }
}
