using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace OffroadAdventure.Models
{
    public class Komentar
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(user))]
        public string autor_id { get; set; }
        [ForeignKey("Komentar")]
        public int komentarId { get; set; }

        [Required(ErrorMessage = "Ocjena je obavezna.")]
        [Range(1, 5, ErrorMessage = "Ocjena mora biti između 1 i 5.")]
        public int ocjena { get; set; }

        [Required(ErrorMessage = "Komentar ne može biti prazan.")]
        [MinLength(3, ErrorMessage = "Komentar mora imati barem 3 znaka.")]
        [MaxLength(1000, ErrorMessage = "Komentar ne može imati više od 1000 znakova.")]
        public string tekst { get; set; }
        public DateTime datum { get; set; } = DateTime.Now;

        public User user { get; set; }  
        public Komentar() { }

    }
}
