namespace QMS.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using QMS.Models;

    public class QmsDbContext : IdentityDbContext<User>
    {
        public QmsDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static QmsDbContext Create()
        {
            return new QmsDbContext();
        }
    }
}
