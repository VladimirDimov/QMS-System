namespace QMS.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;
    using System;
    using Helpers;

    public sealed class Configuration : DbMigrationsConfiguration<QmsDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(QmsDbContext context)
        {
            if (!context.Roles.Any())
            {
                this.SeedRoles(context);
            }

            if (!context.Users.Any())
            {
                this.SeedAdmin(context);
                this.SeedUsers(context);
            }
        }

        private void SeedUsers(QmsDbContext context)
        {
            var store = new UserStore<User>(context);
            var manager = new UserManager<User>(store);
            for (int i = 0; i < 10; i++)
            {
                var user = new User { UserName = $"user_{i}", Email = $"user_{i}@site.com" };

                manager.Create(user, $"User_{i}");
            }

            context.SaveChanges();
        }

        private void SeedAdmin(QmsDbContext context)
        {
            var store = new UserStore<User>(context);
            var manager = new UserManager<User>(store);
            var user = new User { UserName = "admin@admin.com", Email = "admin@admin.com" };

            manager.Create(user, "Admin_1");
            manager.ChangePassword(user.Id, "Admin_1", "admin");
            manager.AddToRole(user.Id, "admin");
        }

        private void SeedRoles(QmsDbContext context)
        {
            var store = new RoleStore<IdentityRole>(context);
            var manager = new RoleManager<IdentityRole>(store);

            var roles = new string[]
            {
                "admin",
                "manage-all-areas",
                "admin-documents",
                "admin-divisions",
                "admin-areas",
                "admin-users",
                "admin-departments",
                "admin-procedures"
            };

            foreach (var role in roles)
            {
                var newRole = new IdentityRole { Name = role };
                manager.Create(newRole);
            }
        }
    }
}
