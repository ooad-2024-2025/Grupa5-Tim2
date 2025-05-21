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
        [ForeignKey("User")]
        public string autor_id { get; set; }
        public int ocjena { get; set; }
        public string tekst { get; set; }
        public DateTime datum { get; set; }

        public User user { get; set; }  
        public Komentar() { }
    }
}
