using Nancy;

namespace Examples.Web.Modules
{
    public class HomeModule : NancyModule
    {

        public HomeModule()
        {
            Get["/"] = _ => View["Index.sshtml"];
        }

    }
}
