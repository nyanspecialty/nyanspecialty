
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NyanSpecialty.Assistance.Web.Models
{
    [Table("WorkFlowSteps")]
    public class WorkFlowStep : CommonProps
    {
        [Key]
        public long WorkFlowStepId { get; set; }
        public long? WorkFlowId { get; set; }
        public long? StatusId { get; set; }
        public long? StepOrder { get; set; }
    }
}
