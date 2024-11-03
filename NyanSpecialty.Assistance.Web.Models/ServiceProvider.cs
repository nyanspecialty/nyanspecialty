using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Runtime.CompilerServices;
namespace NyanSpecialty.Assistance.Web.Models
{
    [Table("ServiceProvider")]
    public class ServiceProvider : CommonProps
    {
        [Key]
        public long ProviderId { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string ServiceArea { get; set; }
        public string AvailabilityStatus { get; set; }
        public string Rating { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Address { get; set; }
    }
}
