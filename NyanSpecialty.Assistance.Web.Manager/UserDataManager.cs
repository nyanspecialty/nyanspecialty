using NyanSpecialty.Assistance.Web.Data.DBConfiguration;
using NyanSpecialty.Assistance.Web.Data.Utility;
using NyanSpecialty.Assistance.Web.Models;

namespace NyanSpecialty.Assistance.Web.Manager
{
    public class UserDataManager : IUserDataManager
    {
        private readonly ApplicationDBContext _dbContext;
        public UserDataManager(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<IEnumerable<UserInfirmation>> FetchUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> GetCurrentUserAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<User> InsertOrUpdateUserAsync(UserRegistration userRegistration)
        {
            if (userRegistration != null)
            {
                if (userRegistration.Id > 0)
                {
                    var dbUser = await _dbContext.users.FindAsync(userRegistration.Id);
                    if (dbUser != null)
                    {
                        dbUser.FirstName = userRegistration.FirstName;
                        dbUser.LastName = userRegistration.LastName;
                        dbUser.Email = userRegistration.Email;
                        dbUser.Phone = userRegistration.Phone;
                        dbUser.RoleId = userRegistration.RoleId;
                        dbUser.CustomerId = userRegistration.CustomerId; // Ensure this matches your class definition
                        dbUser.ProviderId = userRegistration.ProviderId; // Ensure this matches your class definition
                        dbUser.IsBlocked = userRegistration.IsBlocked;
                        dbUser.LastPasswordChangedOn = userRegistration.LastPasswordChangedOn;
                        dbUser.ModifiedBy = userRegistration.ModifiedBy;
                        dbUser.ModifiedOn = DateTimeOffset.UtcNow; // Set to current time
                        dbUser.IsActive = userRegistration.IsActive
                        await _dbContext.SaveChangesAsync();
                    }
                    return dbUser;
                }
                else
                {
                    User user = new User()
                    {
                        FirstName = userRegistration.FirstName,
                        LastName = userRegistration.LastName,
                        Email = userRegistration.Email,
                        Phone = userRegistration.Phone,
                        RoleId = userRegistration.RoleId,
                        CustomerId = userRegistration.CustomerId,
                        ProviderId = userRegistration.ProviderId,
                        IsBlocked = userRegistration.IsBlocked,
                        LastPasswordChangedOn = userRegistration.LastPasswordChangedOn,
                        CreatedBy = userRegistration.CreatedBy,
                        CreatedOn = userRegistration.CreatedOn,
                        ModifiedBy = userRegistration.ModifiedBy,
                        ModifiedOn = userRegistration.ModifiedOn,
                        IsActive = userRegistration.IsActive
                    };
                    if (!string.IsNullOrEmpty(userRegistration.Password))
                    {
                        PasswordManagerUtility amplifyHashSalt = PasswordManagerUtility.GenerateSaltedHash(userRegistration.Password);
                        user.PasswordHash = amplifyHashSalt.Hash;
                        user.PasswordSalt = amplifyHashSalt.Salt;
                    }
                    else
                    {
                        userRegistration.Password = "Admin@2021";
                        PasswordManagerUtility amplifyHashSalt = PasswordManagerUtility.GenerateSaltedHash(userRegistration.Password);
                        user.PasswordHash = amplifyHashSalt.Hash;
                        user.PasswordSalt = amplifyHashSalt.Salt;
                    }
                    await _dbContext.users.AddAsync(user);
                    await _dbContext.SaveChangesAsync();
                    return user;

                }
            }
            return null;
        }
    }
}
