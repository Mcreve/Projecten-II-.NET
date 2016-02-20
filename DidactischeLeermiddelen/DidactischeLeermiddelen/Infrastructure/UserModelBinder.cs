using System.Web.Mvc;
using DidactischeLeermiddelen.Models.Domain;

namespace DidactischeLeermiddelen.Infrastructure
{
    public class UserModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (controllerContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var userList = (IUserRepository) DependencyResolver.Current.GetService(typeof (IUserRepository));
                return userList.FindBy(controllerContext.HttpContext.User.Identity.Name);
            }
            return null;
        }
    }
}