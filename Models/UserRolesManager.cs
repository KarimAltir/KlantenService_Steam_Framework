using System.ComponentModel.DataAnnotations.Schema;

namespace KlantenService_Steam_Framework.Models
{
    [NotMapped]
    public class UserRolesManager
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
