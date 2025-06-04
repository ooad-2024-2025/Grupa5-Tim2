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
        [Required(ErrorMessage = "Polje mora biti popunjeno")]
        [FutureDate(ErrorMessage = "Datum početka mora biti u budućnosti ili danas.")]
        public DateTime datumOd { get; set; }
        [Required(ErrorMessage = "Polje mora biti popunjeno")]
        [FutureDate(ErrorMessage = "Datum početka mora biti u budućnosti ili danas.")]
        public DateTime datumDo { get; set; }
        public int brojVozila { get; set; }

        public StatusZahtjeva status { get; set; }
        [EnumDataType(typeof(StatusZahtjeva))] public StatusZahtjeva statusZahtjeva { get; set; }
        public double popust { get; set; }

        public double cijena { get; set; }
        public string ime { get; set; }

        public string prezime { get; set; } 
        
        public string email { get; set; }   

        public string brojTelefona { get; set; }    

        public string dodatniZahtjev {  get; set; } 
        public ZahtjevZaRentanje() { }

        public User User { get; set; }  

    }
}
