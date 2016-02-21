namespace QMS.Services.Contracts
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;
    using System.Linq;

    public interface IRolesServices
    {
        void AddRoleToUser(string userId, string roleId);

        IQueryable<IdentityRole> All();

        string GetRoleNameById(string roleId);

        ICollection<IdentityUserRole> GetUserRoles(string id);

        void RemoveUserRole(string userId, string roleId);
    }
}