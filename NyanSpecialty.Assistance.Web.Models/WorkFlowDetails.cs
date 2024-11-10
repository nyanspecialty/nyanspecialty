using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyanSpecialty.Assistance.Web.Models
{
    public class WorkFlowDetails
    {
        public WorkFlow workFlow { get; set; }
        public List<WorkFlowStep> workFlowSteps { get; set; }
    }
}
