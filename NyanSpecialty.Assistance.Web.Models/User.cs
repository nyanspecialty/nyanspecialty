using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NyanSpecialty.Assistance.Web.Models
{
    [Table("User")]
    public class User : CommonProps
    {
        [Key]
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public long? CustomerId { get; set; }
        public long? ProviderId { get; set; }
        public long? RoleId { get; set; }
        public DateTimeOffset? LastPasswordChangedOn { get; set; }
        public bool? IsBlocked { get; set; }

    }
}
