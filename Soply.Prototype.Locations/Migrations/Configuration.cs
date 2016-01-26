using Soply.Prototype.Locations.Data;

namespace Soply.Prototype.Locations.Migrations
{
    using System.Data.Entity.Migrations;
    internal sealed class Configuration : DbMigrationsConfiguration<LocationsDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(LocationsDbContext context)
        {

        }       

    }
}
