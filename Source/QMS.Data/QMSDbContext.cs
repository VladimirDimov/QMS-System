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

        public virtual IDbSet<Procedure> Procedures { get; set; }

        public virtual IDbSet<Note> Notes { get; set; }

        public virtual IDbSet<Post> Posts { get; set; }

        public virtual IDbSet<Record> Records { get; set; }

        public virtual IDbSet<RecordFile> RecordFiles { get; set; }

        public virtual IDbSet<Message> Messages { get; set; }

        public static QmsDbContext Create()
        {
            return new QmsDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Record>()
            //.HasOptional(f => f.RecordFile)
            //.WithRequired(s => s.Record);

            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }
    }
}
