namespace QMS.Services
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using QMS.Data;
    using Data.Repository;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RolesServices
    {
        private IQmsData data;

        public RolesServices(IQmsData data)
        {
            this.data = data;
        }

        public IQueryable<IdentityRole> All()
        {
            return this.data.RolesManager.Roles;
        }

        public ICollection<IdentityUserRole> GetUserRoles(string id)
        {
            return this.data.Users.GetById(id).Roles;
        }

        public void AddRoleToUser(string userId, string roleId)
        {
            var roleName = this.GetRoleNameById(roleId);
            var role = this.data.UserManager.AddToRole(userId, roleName);
        }

        public void RemoveUserRole(string userId, string roleId)
        {
            var roleName = this.GetRoleNameById(roleId);
            this.data.UserManager.RemoveFromRole(userId, roleName);
        }

        public string GetRoleNameById(string roleId)
        {
            var roleName = this.data.RolesManager.Roles.FirstOrDefault(r => r.Id == roleId).Name;
            return roleName;
        }
    }
}
