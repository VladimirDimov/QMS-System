using QMS.Data;
using QMS.Data.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QMS.Web.App_Start
{
    public static class DatabaseConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<QmsDbContext, Configuration>());
            QmsDbContext.Create().Database.Initialize(true);
        }
    }
}