using Microsoft.EntityFrameworkCore;
using NyanSpecialty.Assistance.Web.Data.DBConfiguration;
using NyanSpecialty.Assistance.Web.Data.Manager;
using NyanSpecialty.Assistance.Web.Data.Utility;
using NyanSpecialty.Assistance.Web.Models;

namespace NyanSpecialty.Assistance.Web.Manager
{
    public class RoleDataManagaer : IRoleDataManagaer
    {
        private readonly ApplicationDBContext _dbContext;
        public RoleDataManagaer(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Role>> GetRolesAsync()
        {
            return await _dbContext.roles.ToListAsync();
        }

        public async Task<Role> InsertOrUpdateRoleAsync(Role role)
        {
            if (role != null)
            {
                if (role.RoleId == 0)
                {
                    await _dbContext.roles.AddAsync(role);
                    await _dbContext.SaveChangesAsync();
                    return role;
                }
                else
                {
                    var existingRole = await _dbContext.roles.FindAsync(role.RoleId);
                    if (existingRole != null)
                    {
                        bool hasChanges = EntityUpdater.HasChanges(existingRole, role, nameof(role.CreatedBy), nameof(role.CreatedOn));
                        if (hasChanges)
                        {
                            EntityUpdater.UpdateProperties(existingRole, role, nameof(role.CreatedBy), nameof(role.CreatedOn));
                            await _dbContext.SaveChangesAsync();
                            return role;

                        }
                    }
                }

            }
            return null;
        }
    }
}
