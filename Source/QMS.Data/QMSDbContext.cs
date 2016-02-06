namespace QMS.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using QMS.Models;
    using System.Data.Entity;

    public class QmsDbContext : IdentityDbContext<User>
    {
        public QmsDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Area> Areas { get; set; }

        public virtual IDbSet<Department> Departments { get; set; }

        public virtual IDbSet<Division> Divisions { get; set; }

        public virtual IDbSet<Document> Documents { get; set; }

        public virtual IDbSet<Note> Notes { get; set; }

        public virtual IDbSet<Post> Posts { get; set; }

        public virtual IDbSet<Record> Records { get; set; }

        public static QmsDbContext Create()
        {
            return new QmsDbContext();
        }
    }
}
