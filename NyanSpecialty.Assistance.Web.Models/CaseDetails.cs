
namespace NyanSpecialty.Assistance.Web.Models
{
    public class CaseDetails
    {
        public Case caseDetails { get; set; }
        public List<CaseStatus> caseStatuses { get; set; }
        public List<ServiceProviderAssignment>  serviceProviderAssignment { get; set; }
    }
}
