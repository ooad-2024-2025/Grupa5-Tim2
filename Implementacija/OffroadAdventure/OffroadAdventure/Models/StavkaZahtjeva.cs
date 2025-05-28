using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OffroadAdventure.Models
{
    public class StavkaZahtjeva
    {
        [Key]
        public int id { get; set; }
        [ForeignKey("ZahtjevZaRentanje")]
        public int ZahtjevZaRentanjeId { get; set; }

        [ForeignKey("Vozilo")]
        public int VoziloId { get; set; }

        public Vozilo Vozilo { get; set; }

        public ZahtjevZaRentanje ZahtjevZaRentanje { get; set; }
            
        
        public StavkaZahtjeva() { }
        
    }
}
