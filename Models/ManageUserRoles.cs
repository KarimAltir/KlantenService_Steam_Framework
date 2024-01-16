using System.ComponentModel.DataAnnotations.Schema;

namespace KlantenService_Steam_Framework.Models
{
    [NotMapped]
    public class ManageUserRoles
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool Selected { get; set; }
    }
}
