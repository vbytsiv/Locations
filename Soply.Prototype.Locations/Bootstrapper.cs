using Nancy.Conventions;
using Nancy.Diagnostics;
using Nancy.Hosting.Aspnet;

namespace Soply.Prototype.Locations
{
    public class Bootstrapper: DefaultNancyAspNetBootstrapper
    {
        protected override DiagnosticsConfiguration DiagnosticsConfiguration => new DiagnosticsConfiguration {Password = @"User123"};

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);
            Conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("scripts"));
            Conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("Data"));
        }
    }
}
