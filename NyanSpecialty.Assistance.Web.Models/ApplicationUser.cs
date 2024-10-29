namespace NyanSpecialty.Assistance.Web.Models
{
    public class ApplicationUser
    {
        public long? Id { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public long? CustomerId { get; set; }
        public long? ProviderId { get; set; }
        public long? RoleId { get; set; }
    }
}
