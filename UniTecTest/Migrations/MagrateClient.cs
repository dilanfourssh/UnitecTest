using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace UniTecTest.Migrations
{
    public partial class MagrateClient: DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.users",
                c => new
                {
                    userId = c.Int(nullable: false, identity: true),
                    name = c.String(),
                })
                .PrimaryKey(t => t.userId);

        }

        public override void Down()
        {
            DropTable("dbo.users");
        }
    }
}