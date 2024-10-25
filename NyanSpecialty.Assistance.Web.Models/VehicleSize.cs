using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyanSpecialty.Assistance.Web.Models
{
    [Table("VehicleSize")]
    public class VehicleSize : CommonProps
    {
        [Key]
        public long VehicleSizeId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
