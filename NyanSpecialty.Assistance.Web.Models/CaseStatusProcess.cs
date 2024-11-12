using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyanSpecialty.Assistance.Web.Models
{
    public class CaseStatusProcess : CommonProps
    {
        public long CaseId { get; set; }
        public long StatusId { get; set; }
        public long? AssingTo { get; set; }
        public string Notes { get; set; }
    }
}
