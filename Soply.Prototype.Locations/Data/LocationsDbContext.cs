using System;
using System.Collections.Generic;
using System.Data.Entity;
using MySql.Data.Entity;
using Soply.Prototype.Locations.Model;

namespace Soply.Prototype.Locations.Data
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class LocationsDbContext : DbContext
    {
        public LocationsDbContext() :base("MySQLContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<LocationsDbContext, Migrations.Configuration>("MySQLContext"));

        }


        public DbSet<Location> Locations { get; set; }
    }
}
