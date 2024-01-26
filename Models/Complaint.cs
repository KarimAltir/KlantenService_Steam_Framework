using KlantenService_Steam_Framework.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KlantenService_Steam_Framework.Models
{
    public class Complaint
    {
        public int Id { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }

        // Foreign key naar Game
        [ForeignKey("Game")]
        public int GameId { get; set; }
        [Display(Name = "Game")]
        public string Name { get; set; } 
        // Navigatie-eigenschap naar Game
        public Game? Game { get; set; }

        // Foreign key naar ProblemType
        [ForeignKey("ProblemType")]
        public int ProblemTypeId { get; set; }
        [Display(Name = "ProblemType")]
        public string TypeName { get; set; }
        public ProblemType? ProblemType { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }
        public string Status { get; set; }

        [ForeignKey("KlantenServiceUser")]
        public string UserId { get; set; }
        //adding user name
        public KlantenServiceUser? User { get; set; }
    }
}
