using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NyanSpecialty.Assistance.Web.Models
{
    [Table("ServiceProviderAssignment")]
    public class ServiceProviderAssignment : CommonProps
    {
        [Key]
        public long AssignmentId { get; set; }
        public long? CaseId { get; set; }
        public long? ServiceProviderId { get; set; }
        public DateTimeOffset? AssignedOn { get; set; }
        public string Response { get; set; }
        public DateTimeOffset? ResponseOn { get; set; }
     
    }
}
