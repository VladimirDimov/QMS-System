using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QMS.Data.Repository;
using QMS.Models;

namespace QMS.Data
{
    public interface IQmsData : IDisposable
    {
        IRepository<User> Users { get; }

        int SaveChanges();
    }
}
