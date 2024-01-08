using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KlantenService_Steam_Framework.Models
{
    public class Parameter
    {
        [Key]
        [Display(Name = "Parameter")]
        public string Name { get; set; }

        [Display(Name = "Waarde")]
        public string Value { get; set; }

        [Display(Name = "Beschrijving")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [ForeignKey("KlantenServiceUser")]
        public string UserId { get; set; }

        public DateTime LastChanged { get; set; } = DateTime.Now;

        public DateTime Obsolete { get; set; } = DateTime.MaxValue;

        public string Destination { get; set; }
    }
}
