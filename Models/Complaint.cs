using System.ComponentModel.DataAnnotations.Schema;

namespace KlantenService_Steam_Framework.Models
{
    public class Complaint
    {
        public int Id { get; set; }
        public string Description { get; set; }

        // Foreign key naar Game
        [ForeignKey("Game")]
        public int GameId { get; set; }
        // Navigatie-eigenschap naar Game
        public Game? Game { get; set; }

        // Foreign key naar ProblemType
        [ForeignKey("ProblemType")]
        public int ProblemTypeId { get; set; }
        public ProblemType? ProblemType { get; set; }
        
        public string Email { get; set; }
        public string Status { get; set; }

        ////adding user name
        //public string UserName { get; set; }
    }
}
