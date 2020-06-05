using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using UniTecTest.Migrations;
using UniTecTest.Models;

namespace UniTecTest.Context
{
    public class UniTechTestContext: DbContext
    {
        public UniTechTestContext() : base("connectionString")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<UniTechTestContext, Configuration>());
        }
        public DbSet<EmpMaster> empMasters { get; set; }//madal class file access
        public DbSet<EmployerLeave> employerLeaves { get; set; }//madal class file access

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}