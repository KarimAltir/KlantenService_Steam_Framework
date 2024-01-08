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

        [Display(Name = "Spel optellen")]
        public int Games_count { get; set; }

        // De verzameling Complaints die aan dit Game zijn gekoppeld
        //public ICollection<Complaint> Complaints { get; set; }

    }


}
