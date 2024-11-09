

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NyanSpecialty.Assistance.Web.Models
{
    [Table("FaultType")]
    public class FaultType : CommonProps
    {
        [Key]
        public long FaultTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
       
    }
}
