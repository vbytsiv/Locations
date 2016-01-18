using Nancy;
using Nancy.ModelBinding;
using Soply.Prototype.Locations.Model;

namespace Soply.Prototype.Locations
{
    public class HomeModule: NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => "Hello";
            Get["/Home"] = _ => View["Home"];

            Post["/Home"] =  _ =>
            {
                var location = this.Bind<Location>();
                return View["Home"];
            };
        }
    }
}
