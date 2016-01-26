using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.Diagnostics;
using Nancy.Hosting.Aspnet;
using Nancy.TinyIoc;

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

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
            StaticConfiguration.DisableErrorTraces = false;
        }
    }
}
