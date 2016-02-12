namespace QMS.Services
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using QMS.Data;
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
    }
}
