using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace UniTecTest.Migrations
{
    internal sealed class Configuration: DbMigrationsConfiguration<UniTecTest.Context.UniTechTestContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Tshirt.Context.tshirtContext";
        }

        protected override void Seed(UniTecTest.Context.UniTechTestContext context)
        {

        }
    }
}