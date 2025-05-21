using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OffroadAdventure.Models.Enums;

namespace OffroadAdventure.Models
{
    public class Placanje
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("ZahtjevZaRentanje")]
        public int zahtjevZaRentanjeId { get; set; }
        public NacinPlacanja nacinPlacanja { get; set; }
        public StatusPlacanja status { get; set; }
        public DateTime datumPlacanja { get; set; }
        public double popust { get; set; }

        public ZahtjevZaRentanje zahtjevZaRentanje { get; set; }
        public Placanje() { }
    }
}
