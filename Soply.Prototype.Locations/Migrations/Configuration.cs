using Soply.Prototype.Locations.Data;
using Soply.Prototype.Locations.Model;

namespace Soply.Prototype.Locations.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LocationsDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(LocationsDbContext context)
        {
            context.Locations.AddOrUpdate(new Location
            {
                Id = 1,
                KeyWords = "one two three",
                Latitude = "10.123456",
                Longitude = "20.123456",
                Title1 = "Initial point"
            });
        }
    }
}
