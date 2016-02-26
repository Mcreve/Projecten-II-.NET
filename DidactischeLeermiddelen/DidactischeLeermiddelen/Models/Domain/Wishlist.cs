using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;

namespace DidactischeLeermiddelen.Models.Domain
{
    /// <summary>
    /// This class stores the WishlistLines and provides operations to add, remove, increase and decrease these WishlistLines.
    /// </summary>
    public class Wishlist
    {
        /// <summary>
        /// The WishlistLines stored in this object
        /// </summary>
        #region Properties
        private IList<WishlistLine> lines = new List<WishlistLine>();
        /// <summary>
        /// Getter for the lines field.
        /// </summary>
        public IEnumerable<WishlistLine> WishlistLines { get { return lines.AsEnumerable(); } }
        /// <summary>
        /// The number of WishlistLines (items) in this object.
        /// </summary>
        public int NumberOfItems { get { return lines.Count(); } }
        #endregion

        #region Methods
        /// <summary>
        /// This method adds a LearningUtility to the lines list. If the item is allready in the list an exception is thrown.
        /// </summary>
        /// <param name="item">The item that should be added to the list.</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void AddLine(LearningUtilityDetails item)
        {
            WishlistLine line = GetWishlistLine(item.Id);
            if(line == null)
                lines.Add(new WishlistLine {LearningUtility = item, Quantity = 1});
            else
                throw new InvalidOperationException("Item werd reeds aan uw verlanglijstje toegevoegd.");
        }

        /// <summary>
        /// This method removes a LearningUtility from the lines list. If the item is not found in the list an exception is thrown.
        /// </summary>
        /// <param name="item">The item that should be removed from the list.</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void RemoveLine(LearningUtilityDetails item)
        {
            WishlistLine line = GetWishlistLine(item.Id);
            if (line != null)
                lines.Remove(line);
            else
                throw new InvalidOperationException("Item werd niet in uw verlanglijstje terug gevonden.");
        }

        /// <summary>
        /// This method increases the item in the lines list with 1. If the item is not found in the list an exception is thrown.
        /// </summary>
        /// <param name="learningUtilityDetailsId">The Id from the item to be increased</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void IncreaseQuantity(int learningUtilityDetailsId)
        {
            WishlistLine line = GetWishlistLine(learningUtilityDetailsId);
            if (line != null)
                line.Quantity++;
            else
                throw new InvalidOperationException("Item werd niet in uw verlanglijstje terug gevonden.");
        }

        /// <summary>
        /// This method decreases the item in the lines list with 1. If the item is not found in the list an exception is thrown.
        /// Should the items quantity drop below 1, the line is removed.
        /// </summary>
        /// <param name="learningUtilityDetailsId">The Id from the item to be decreased</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void DecreaseQuantity(int learningUtilityDetailsId)
        {
            WishlistLine line = GetWishlistLine(learningUtilityDetailsId);
            if (line != null)
            {
                line.Quantity--;
                if (line.Quantity < 1)
                    lines.Remove(line);
            }
            else
                throw new InvalidOperationException("Item werd niet in uw verlanglijstje terug gevonden.");
        }


        /// <summary>
        /// This method tries to find the item in the lines list and returns it.
        /// </summary>
        /// <param name="itemId">The id from the item to be found</param>
        /// <returns>A WishlistLine</returns>
        private WishlistLine GetWishlistLine(int itemId)
        {
            return lines.SingleOrDefault(l => l.LearningUtility.Id == itemId);
        }
        #endregion
    }
}