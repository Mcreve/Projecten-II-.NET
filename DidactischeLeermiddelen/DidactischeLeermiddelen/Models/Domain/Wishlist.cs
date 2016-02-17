using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DidactischeLeermiddelen.Models.Domain
{
    public class Wishlist
    {
        public IList<WishlistLine>  WishlistLines { get; set; }
    }
}