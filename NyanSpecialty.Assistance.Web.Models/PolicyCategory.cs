using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NyanSpecialty.Assistance.Web.Models
{
    [Table("PolicyCategory")]
    public class PolicyCategory : CommonProps
    {
        [Key]
        public long PolicyCategoryId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
