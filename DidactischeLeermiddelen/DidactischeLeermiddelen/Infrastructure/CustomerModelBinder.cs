using System.Web.Mvc;
using DidactischeLeermiddelen.Models.Domain.Interfaces;

namespace DidactischeLeermiddelen.Infrastructure
{ 
    public class CustomerModelBinder : IModelBinder
{
    public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
    {
        if (controllerContext.HttpContext.User.Identity.IsAuthenticated)
        {
            ICustomerRepository repos = (ICustomerRepository)DependencyResolver.Current.GetService(typeof(ICustomerRepository));
            return repos.FindByEmail(controllerContext.HttpContext.User.Identity.Name);
        }
        return null;
    }
}
}
