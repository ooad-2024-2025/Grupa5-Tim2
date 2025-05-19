using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OffroadAdventure.Models
{
    public class Vozilo
    {
        [Key]
        public int id { get; set; }
        public string model { get; set; }
        public string tip { get; set; }
        public double cijenaPoDanu { get; set; }
        public Boolean dostupno { get; set; }
        public string slikaURL { get; set; }
        
        public Vozilo() { }
    }
}
