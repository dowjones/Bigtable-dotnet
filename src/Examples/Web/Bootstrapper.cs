using Autofac;
using Nancy.Bootstrappers.Autofac;
using Nancy.Conventions;

namespace Examples.Web
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        private readonly ILifetimeScope _container;

        public Bootstrapper(ILifetimeScope container)
        {
            _container = container;
        }


        protected override void ConfigureConventions(NancyConventions conventions)
        {
            // See http://beletsky.net/2012/08/customizing-folders-layout-for-nancyfx.html

            // static content
            conventions.StaticContentsConventions.Clear();
            conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("Content", "Content"));

            // views
            conventions.ViewLocationConventions.Clear();
            conventions.ViewLocationConventions.Add((viewName, model, context) => string.Concat("Views/", context.ModuleName, "/", viewName));
            conventions.ViewLocationConventions.Add((viewName, model, context) => string.Concat("Views/Shared/", viewName));
        }


        protected override ILifetimeScope GetApplicationContainer()
        {
            return _container;
        }
    }
}
