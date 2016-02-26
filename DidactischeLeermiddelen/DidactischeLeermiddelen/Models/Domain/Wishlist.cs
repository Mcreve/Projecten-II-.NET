using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;

namespace DidactischeLeermiddelen.Models.Domain
{
    public class Wishlist
    {
        #region Properties
        private IList<WishlistLine> lines = new List<WishlistLine>();
        public IEnumerable<WishlistLine> WishlistLines { get { return lines.AsEnumerable(); } }
        public int NumberOfItems { get { return lines.Count(); } }
        #endregion

        #region Methods

        public void AddLine(LearningUtilityDetails item)
        {
            WishlistLine line = GetWishlistLine(item.Id);
            if(line == null)
                lines.Add(new WishlistLine {LearningUtility = item, Quantity = 1});
        }

        public void RemoveLine(LearningUtilityDetails item)
        {
            WishlistLine line = GetWishlistLine(item.Id);
            if (line != null)
                lines.Remove(line);
        }

        public void IncreaseQuantity(int learningUtilityDetailsId)
        {
            WishlistLine line = GetWishlistLine(learningUtilityDetailsId);
            if (line != null)
                line.Quantity++;
        }

        public void DecreaseQuantity(int learningUtilityDetailsId)
        {
            WishlistLine line = GetWishlistLine(learningUtilityDetailsId);
            if (line != null)
            {
                line.Quantity--;
                if (line.Quantity < 1)
                    lines.Remove(line);
            }
        }

        private WishlistLine GetWishlistLine(int itemId)
        {
            return lines.SingleOrDefault(l => l.LearningUtility.Id == itemId);
        }
        #endregion
    }
}