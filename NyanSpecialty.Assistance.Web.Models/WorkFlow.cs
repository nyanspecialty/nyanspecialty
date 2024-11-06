
using System.ComponentModel.DataAnnotations.Schema;

namespace NyanSpecialty.Assistance.Web.Models
{
    [Table("WorkFlow")]
    public class WorkFlow : CommonProps
    {
        public long WorkFlowId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
