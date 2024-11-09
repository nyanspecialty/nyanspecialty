
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NyanSpecialty.Assistance.Web.Models
{
    [Table("CaseStatus")]
    public class CaseStatus : CommonProps
    {
        [Key]
        public long CaseStatusId { get; set; }
        public long? CaseId { get; set; }
        public long? StatusId { get; set; }
        public string Notes { get; set; }
      
    }
}
