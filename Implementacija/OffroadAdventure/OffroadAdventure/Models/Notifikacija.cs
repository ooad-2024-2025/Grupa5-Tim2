using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OffroadAdventure.Models.Enums; 

namespace OffroadAdventure.Models
{
    public class Notifikacija
    {
        [Key]
        public int id { get; set; }
        [ForeignKey("User")]
        public string primalac_id { get; set; }
        public string tekst { get; set; }
        public DateTime datum { get; set; }
        public StatusNotifikacije status { get; set; }
        [EnumDataType(typeof(StatusNotifikacije))] public StatusNotifikacije statusNotifikacije { get; set; }
        public Notifikacija() { }
        
        public User User { get; set; }  
    }
}
