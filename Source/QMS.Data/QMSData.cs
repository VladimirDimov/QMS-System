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

        public IRepository<Area> Areas
        {
            get
            {
                return this.GetRepository<Area>();
            }
        }

        public IRepository<Department> Departments
        {
            get
            {
                return this.GetRepository<Department>();
            }
        }

        public IRepository<Division> Divisions
        {
            get
            {
                return this.GetRepository<Division>();
            }
        }

        public IRepository<Document> Documents
        {
            get
            {
                return this.GetRepository<Document>();
            }
        }

        public IRepository<Procedure> Procedures
        {
            get
            {
                return this.GetRepository<Procedure>();
            }
        }

        public IRepository<Note> Notes
        {
            get
            {
                return this.GetRepository<Note>();
            }
        }

        public IRepository<Post> Posts
        {
            get
            {
                return this.GetRepository<Post>();
            }
        }

        public IRepository<Record> Records
        {
            get
            {
                return this.GetRepository<Record>();
            }
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
