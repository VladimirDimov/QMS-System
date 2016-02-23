namespace QMS.Web.App_Start
{
    using QMS.Data;
    using QMS.Data.Migrations;
    using System.Data.Entity;

    public static class DatabaseConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<QmsDbContext, Configuration>());
            QmsDbContext.Create().Database.Initialize(true);
        }
    }
}