using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NyanSpecialty.Assistance.Web.Models
{
    [Table("Case")]
    public class Case : CommonProps
    {
        [Key]
        public long CaseId { get; set; }
        public long? InsurancePolicyId { get; set; }
        public string? Description { get; set; }
        public string? CustomerName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? CurrentLocation { get; set; }
        public string? Langitude { get; set; }
        public string? Latitude { get; set; }
        public long? ServiceTypeId { get; set; }
        public long? StatusId { get; set; }
        public long? ServiceProviderId { get; set; }
        public DateTimeOffset? ServiceRequestDate { get; set; }
        public DateTimeOffset? ResponseTime { get; set; }
        public DateTimeOffset? CompletionTime { get; set; }
        public int? Rating { get; set; }
        public string? Feedback { get; set; }
        public int? Priority { get; set; }
        public string? Notes { get; set; }
      
    }
}
