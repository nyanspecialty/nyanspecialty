using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyanSpecialty.Assistance.Web.Models
{
    public class UserInfirmation : CommonProps
    {
        public long? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public long? CustomerId { get; set; }
        public long? ProviderId { get; set; }
        public long? RoleId { get; set; }
        public DateTimeOffset? LastPasswordChangedOn { get; set; }
        public bool? IsBlocked { get; set; }

    }
}
