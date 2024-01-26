using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Security.Policy;

namespace KlantenService_Steam_Framework.Models
{
    public class Game
    {

        //TODO: contolleren voor de namen van de attributen
        public int Id { get; set; }

        [Display(Name = "Naam")]
        public string Name { get; set; }

        [Display(Name = "Afbeelding")]
        public string Background_image { get; set; }

        [Display(Name = "Releasedatum")]
        public string Released { get; set; }

        //rating
        [Display(Name = "Rating")]
        public double Rating { get; set; }

        //slug
        [Display(Name = "Slug")]
        public string Slug { get; set; }

        // De verzameling Complaints die aan dit Game zijn gekoppeld
        //public ICollection<Complaint> Complaints { get; set; }

    }


}
