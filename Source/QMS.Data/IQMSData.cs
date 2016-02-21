using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QMS.Data.Repository;
using QMS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace QMS.Data
{
    public interface IQmsData : IDisposable
    {
        IRepository<User> Users { get; }

        IRepository<Area> Areas { get; }

        IRepository<Department> Departments { get; }

        IRepository<Division> Divisions { get; }

        IRepository<Document> Documents { get; }

        IRepository<Note> Notes { get; }

        IRepository<Post> Posts { get; }

        IRepository<Record> Records { get; }

        IRepository<Procedure> Procedures { get; }

        IRepository<RecordFile> RecordFiles { get; }

        IRepository<Message> Messages { get; }

        IRepository<Log> Logs { get; }

        int SaveChanges();

        RoleManager<IdentityRole> RolesManager { get; }

        UserManager<User> UserManager { get; }
    }
}
