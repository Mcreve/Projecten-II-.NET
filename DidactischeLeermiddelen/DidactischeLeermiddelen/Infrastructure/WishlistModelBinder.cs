using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DidactischeLeermiddelen.Models.Domain;

namespace DidactischeLeermiddelen.Infrastructure
{
    public class WishlistModelBinder : IModelBinder
    {
        private const string wishlistSessionKey = "wishlist";
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Wishlist wishlist = controllerContext.HttpContext.Session[wishlistSessionKey] as Wishlist;
            if (wishlist == null)
            {
                wishlist = new Wishlist();
                controllerContext.HttpContext.Session[wishlistSessionKey] = wishlist;
            }
            return wishlist;
        }
    }
}