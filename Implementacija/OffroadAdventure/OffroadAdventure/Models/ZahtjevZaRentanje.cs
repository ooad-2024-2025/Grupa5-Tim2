using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OffroadAdventure.Models.Enums;

namespace OffroadAdventure.Models
{
    public class ZahtjevZaRentanje
    {
        [Key]
        public int id { get; set; }
        
        [ForeignKey("User")]
        public string korisnik_id { get; set; }
        public DateTime datumOd { get; set; }
        public DateTime datumDo { get; set; }
        public int brojVozila { get; set; }

        public StatusZahtjeva status { get; set; }
        [EnumDataType(typeof(StatusZahtjeva))] public StatusZahtjeva statusZahtjeva { get; set; }
        public double popust { get; set; }
        public double vrijemeTrajanja { get; set; }

        public ZahtjevZaRentanje() { }

        public User User { get; set; }  

    }
}
