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
        public string? korisnik_id { get; set; }
        [Required(ErrorMessage = "Polje mora biti popunjeno")]
        [FutureDate(ErrorMessage = "Datum početka mora biti u budućnosti ili danas.")]
        public DateTime datumOd { get; set; }
        [Required(ErrorMessage = "Polje mora biti popunjeno")]
        [FutureDate(ErrorMessage = "Datum početka mora biti u budućnosti ili danas.")]
        public DateTime datumDo { get; set; }
        public int brojVozila { get; set; }
        [EnumDataType(typeof(StatusZahtjeva))] public StatusZahtjeva statusZahtjeva { get; set; }
        public double popust { get; set; }

        public double cijena { get; set; }

        [Required(ErrorMessage = "Ime je obavezno")]
        [RegularExpression("^[A-Za-zšđčćžŠĐČĆŽ]+$", ErrorMessage = "Ime smije sadržavati samo slova")]
        public string ime { get; set; }

        [Required(ErrorMessage = "Prezime je obavezno")]
        [RegularExpression("^[A-Za-zšđčćžŠĐČĆŽ]+$", ErrorMessage = "Prezime smije sadržavati samo slova")]
        public string prezime { get; set; }

        [Required(ErrorMessage = "Email je obavezan")]
        [EmailAddress(ErrorMessage = "Unesite ispravan email")]
        [RegularExpression(@"^\S+$", ErrorMessage = "Email ne smije sadržavati razmake")]
        public string email { get; set; }

        [Required(ErrorMessage = "Broj telefona je obavezan")]
        [RegularExpression(@"^\d{9,10}$", ErrorMessage = "Broj telefona mora imati 9 ili 10 cifara")]
        public string brojTelefona { get; set; }
        public ICollection<StavkaZahtjeva> Stavke { get; set; }

        public string? dodatniZahtjev {  get; set; } 
        public ZahtjevZaRentanje() { }

        public Placanje? Placanje { get; set; }

        public User User { get; set; }  

    }
}
