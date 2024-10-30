using NyanSpecialty.Assistance.Web.Models;

namespace NyanSpecialty.Assistance.Web.Data.Manager
{
    public interface IAuthenticationDataManager
    {
        Task<AuthResponse> AuthenticateUserAsync(UserAuthentication authentication);
        Task<ApplicationUser> GenarateUserClaimsAsync(AuthResponse auth);
    }
}
