using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NyanSpecialty.Assistance.Web.Models
{
    [Table("Role")]
    public class Role : CommonProps
    {
        [Key]
        public long RoleId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
