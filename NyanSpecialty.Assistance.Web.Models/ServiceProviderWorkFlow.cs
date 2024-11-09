
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NyanSpecialty.Assistance.Web.Models
{
    [Table("ServiceProviderWorkFlow")]
    public class ServiceProviderWorkFlow : CommonProps
    {
        [Key]
        public long ServiceProviderWorkFlowId { get; set; }
        public long? WorkFlowId { get; set; }
        public long? ServiceProviderId { get; set; }
        public DateTimeOffset? AssignedOn { get; set; } 
        public long? LastWorkFlowId { get; set; }
      
    }
}
