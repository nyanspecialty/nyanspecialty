using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyanSpecialty.Assistance.Web.Models
{
    [Table("InsurancePolicy")]
    public class InsurancePolicy : CommonProps
    {
        [Key]
        public long InsurancePolicyId { get; set; } // BIGINT
        public string PolicyReference { get; set; } // VARCHAR(50)
        public string VIN { get; set; } // VARCHAR(20)
        public string CarRegistrationNo { get; set; } // VARCHAR(20)
        public string VehicleClass { get; set; } // VARCHAR(50)
        public string VehicleSize { get; set; } // VARCHAR(20)
        public string CustomerName { get; set; } // VARCHAR(100)
        public string CustomerPhone { get; set; } // VARCHAR(20)
        public string CustomerEmail { get; set; } // VARCHAR(100)
        public decimal? CustomerLifetimeValue { get; set; } // DECIMAL(10, 2)
        public char? Gender { get; set; } // CHAR(1)
        public string Education { get; set; } // VARCHAR(50)
        public string EmploymentStatus { get; set; } // VARCHAR(50)
        public decimal? Income { get; set; } // DECIMAL(10, 2)
        public string MaritalStatus { get; set; } // VARCHAR(10)
        public string PolicyType { get; set; } // VARCHAR(50)
        public string Policy { get; set; } // VARCHAR(50)
        public string Coverage { get; set; } // VARCHAR(50)
        public DateTimeOffset? EffectiveToDate { get; set; } // DATETIMEOFFSET
        public decimal? MonthlyPremiumAuto { get; set; } // DECIMAL(10, 2)
        public int? MonthsSinceLastClaim { get; set; } // INT
        public int? MonthsSincePolicyInception { get; set; } // INT
        public int? NumberOfOpenComplaints { get; set; } // INT
        public int? NumberOfPolicies { get; set; } // INT
        public string RenewOfferType { get; set; } // VARCHAR(50)
        public string SalesChannel { get; set; } // VARCHAR(50)
        public decimal? TotalClaimAmount { get; set; } // DECIMAL(10, 2)
        public string State { get; set; } // VARCHAR(50)
        public string LocationCode { get; set; } // VARCHAR(10)
        public string Response { get; set; } // VARCHAR(10)
        
    }
}
