using QMS.Data;
using QMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.Services
{
    public class UsersServices
    {
        private IQmsData data;

        public UsersServices(IQmsData data)
        {
            this.data = data;
        }

        public IQueryable<User> All()
        {
            return this.data.Users.All();
        }
    }
}
