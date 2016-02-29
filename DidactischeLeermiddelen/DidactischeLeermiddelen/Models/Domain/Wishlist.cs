using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities.LearningUtilityStates;

namespace DidactischeLeermiddelen.Models.Domain
{
    /// <summary>
    /// This class stores the LearningUtilities and provides operations to add and remove them.
    /// </summary>
    public class Wishlist
    {
        /// <summary>
        /// The LearningUtilities stored in this object
        /// </summary>
        #region Properties
        private IList<LearningUtilityDetails> learningUtilities = new List<LearningUtilityDetails>();
        /// <summary>
        /// Getter for the lines field.
        /// </summary>
        public IEnumerable<LearningUtilityDetails> LearningUtilities { get { return learningUtilities.AsEnumerable(); } }
        /// <summary>
        /// The number of WishlistLines (items) in this object.
        /// </summary>
        public int NumberOfItems { get { return learningUtilities.Count(); } }
        #endregion

        #region Methods
        /// <summary>
        /// This method adds a LearningUtility to the list. If the item is allready in the list an exception is thrown.
        /// </summary>
        /// <param name="item">The item that should be added to the list.</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void AddItem(LearningUtilityDetails item)
        {
            LearningUtilityDetails learningUtility = FindLearningUtility(item.Id);
            if (learningUtility == null)
                learningUtilities.Add(item);
            else
                throw new InvalidOperationException("Item werd reeds aan uw verlanglijstje toegevoegd.");
        }

        /// <summary>
        /// This method removes a LearningUtility from the list. If the item is not found in the list an exception is thrown.
        /// </summary>
        /// <param name="item">The item that should be removed from the list.</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void RemoveItem(LearningUtilityDetails item)
        {
            LearningUtilityDetails learningUtility = FindLearningUtility(item.Id);
            if (learningUtility != null)
                learningUtilities.Remove(learningUtility);
            else
                throw new InvalidOperationException("Item werd niet in uw verlanglijstje terug gevonden.");
        }

        private LearningUtilityDetails FindLearningUtility(int id)
        {
            return learningUtilities.SingleOrDefault(l => l.Id == id);
        }
        #endregion
    }
}