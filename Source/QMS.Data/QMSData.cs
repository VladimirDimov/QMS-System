using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QMS.Data.Repository;
using QMS.Models;

namespace QMS.Data
{
    public class QmsData : IQmsData
    {
        private QmsDbContext context;
        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public QmsData()
            : this(new QmsDbContext())
        {
        }

        public QmsData(QmsDbContext context)
        {
            this.context = context;
        }

        public IRepository<User> Users
        {
            get
            {
                return this.GetRepository<User>();
            }
        }

        public void Dispose()
        {
            if (this.context != null)
            {
                this.context.Dispose();
            }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(EfGenericRepository<T>);

                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }
    }
}
