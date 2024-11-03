using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyanSpecialty.Assistance.Web.Models
{
    [Table("ServiceType")]
    public class ServiceType : CommonProps
    {
        [Key]
        public long ServiceTypeId { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }
    }
}
