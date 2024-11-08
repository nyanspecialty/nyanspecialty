using NyanSpecialty.Assistance.Web.Models;

namespace NyanSpecialty.Assistance.Web.Data.Manager
{
    public interface IRoleDataManagaer
    {
        Task<Role> InsertOrUpdateRoleAsync(Role role);
        Task<List<Role>> GetRolesAsync();
    }
}
