using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyanSpecialty.Assistance.Web.Models
{
    [Table("PolicyType")]
    public class PolicyType : CommonProps
    {
        [Key]
        public long PolicyTypeId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
