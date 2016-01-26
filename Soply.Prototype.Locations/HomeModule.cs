using System.Data.Entity;
using System.Linq;
using Nancy;
using Nancy.ModelBinding;
using Soply.Prototype.Locations.Data;
using Soply.Prototype.Locations.Model;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Threading.Tasks;

namespace Soply.Prototype.Locations
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            var searchContext = new ElasticSearchContext("locations", HttpContext.Current.Server.MapPath(@"~\Data\Settings.json"));
            searchContext.SetupIndex();

            Get["/"] = _ => View["Home"];

            Post["/"] = _ =>
            {
                Task.Run(() =>
                {
                    var model = this.Bind<Location>();
                    using (var context = new LocationsDbContext())
                    {
                        context.Locations.Add(model);
                        context.SaveChanges();
                    }
                    searchContext.Add(model);
                });

                return View["Home"];
            };

            Get["/search"] = _ =>
            {
                return View["Search"];
            };

            //todo: this should be DELETE.
            Get["/clear"] = _ =>
            {
                using (var context = new LocationsDbContext())
                {
                    context.Database.Delete();
                }

                searchContext.RemoveIndex();

                return "DB and index cleared.";
            };

            //todo: this should be POST
            Get["/load/{count}"] = _ =>
            {
                var locations = ReadWorldCities(_.count);

                using (var context = new LocationsDbContext())
                {
                    context.Locations.AddRange(locations);
                    context.SaveChanges();
                }

                searchContext.Add(locations);

                return $"{_.count} items inserted.";
            };
        }
        private IEnumerable<Location> ReadWorldCities(int count)
        {
            //todo: use any nice csv mapping utility

            //city,city_ascii,lat,lng,pop,country,iso2,iso3,province
            var i = 0;
            var path = HttpContext.Current.Server.MapPath(@"~/Data/world_cities.txt");
            var locations = File.ReadAllLines(path).Skip(1)
                            .Select(x => x.Split(','))
                            .Select(x => new Location
                            {
                                Id = i++,
                                Latitude = x[2],
                                Longitude = x[3],
                                Title = x[1],
                                Keywords = $"{x[5]} {x[6]} {x[7]}"
                            })
                            .Take(count);
            return locations;

        }
    }
}
