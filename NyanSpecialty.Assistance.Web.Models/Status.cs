using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace NyanSpecialty.Assistance.Web.Models
{
    [Table("Status")]
    public class Status : CommonProps
    {
        [Key]
        public long StatusId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
