using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using MySql.Data.MySqlClient;
using Nancy;
using Nancy.ModelBinding;
using Soply.Prototype.Locations.Data;
using Soply.Prototype.Locations.Model;
using Soply.Prototype.Locations.ViewModel;

namespace Soply.Prototype.Locations
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => View["Home"];

            Post["/"] = _ =>
           {
               var viewModel = this.Bind<LocationViewModel>();
               using (var context = new LocationsDbContext())
               {
                   context.Locations.Add(ToModel(viewModel));
                   context.SaveChanges();
               }

               return View["Home"];
           };

            Get["/search"] = _ =>
            {
                return View["Search"];
            };
        }

        private Location ToModel(LocationViewModel viewModel)
        {
            return new Location
            {
                Latitude = viewModel.Latitude,
                Longitude = viewModel.Longitude,
                Title1 = viewModel.Title,
                KeyWords = viewModel.KeyWords
            };

        }


    }
}
