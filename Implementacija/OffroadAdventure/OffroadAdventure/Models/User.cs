using Microsoft.AspNetCore.Identity;
namespace OffroadAdventure.Models
{
    public class User: IdentityUser
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
       
        public User() { } 
    }
}
