using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DidactischeLeermiddelen.Infrastructure;
using DidactischeLeermiddelen.Models.DAL;
using DidactischeLeermiddelen.Models.Domain;
using DidactischeLeermiddelen.Models.Domain.Users;

namespace DidactischeLeermiddelen
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            LeermiddelenContext db = new LeermiddelenContext();
            db.Database.Initialize(true);

            ModelBinders.Binders.Add(typeof(User), new UserModelBinder());
        }
    }
}
