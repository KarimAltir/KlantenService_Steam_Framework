namespace KlantenService_Steam_Framework.Models
{
    public class ProblemType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }

        // De verzameling Complaints die aan dit probleemtype zijn gekoppeld
        //public ICollection<Complaint> Complaints { get; set; }
    }
}
