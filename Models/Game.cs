using System.Drawing;
using System.Security.Policy;

namespace KlantenService_Steam_Framework.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Background_image { get; set; }
        public int Games_count { get; set; }

        // De verzameling Complaints die aan dit Game zijn gekoppeld
        //public ICollection<Complaint> Complaints { get; set; }

    }
}
