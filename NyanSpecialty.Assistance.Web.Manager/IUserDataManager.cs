using NyanSpecialty.Assistance.Web.Models;


namespace NyanSpecialty.Assistance.Web.Manager
{
    public interface IUserDataManager
    {
        Task<User> InsertOrUpdateUserAsync(UserRegistration user);

        Task<IEnumerable<UserInfirmation>> FetchUsersAsync();

        Task<UserInfirmation> FetchUserByIdAsync(long id);

        Task<ApplicationUser> GetCurrentUserAsync(string email);
    }
}
