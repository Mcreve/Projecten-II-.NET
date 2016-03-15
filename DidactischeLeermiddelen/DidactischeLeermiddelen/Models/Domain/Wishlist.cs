using System;
using System.Collections.Generic;
using System.Linq;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;

namespace DidactischeLeermiddelen.Models.Domain
{
    /// <summary>
    /// This class stores the LearningUtilities and provides operations to add and remove them.
    /// </summary>
    public class Wishlist
    {
        public Wishlist() {
            LearningUtilities = new List<LearningUtility>();
        }
        public int Id { get; set; }
        /// <summary>
        /// The LearningUtilities stored in this object
        /// </summary>
        #region Properties
        //private IList<LearningUtility> learningUtilities = new List<LearningUtility>();
        /// <summary>
        /// Getter for the lines field.
        /// </summary>
        public virtual List<LearningUtility> LearningUtilities
        {
            get;
            set;
        }
        /// <summary>
        /// The number of WishlistLines (items) in this object.
        /// </summary>
        public int NumberOfItems { get { return LearningUtilities.Count(); } }
        #endregion

        #region Methods
        /// <summary>
        /// This method adds a LearningUtility to the list. If the item is allready in the list an exception is thrown.
        /// </summary>
        /// <param name="item">The item that should be added to the list.</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void AddItem(LearningUtility item)
        {
            LearningUtility learningUtility = FindLearningUtility(item.Id);
            if (learningUtility == null)
                LearningUtilities.Add(item);
            else
                throw new InvalidOperationException("Item werd reeds aan uw verlanglijstje toegevoegd.");
        }

        /// <summary>
        /// This method removes a LearningUtility from the list. If the item is not found in the list an exception is thrown.
        /// </summary>
        /// <param name="item">The item that should be removed from the list.</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void RemoveItem(LearningUtility item)
        {
            LearningUtility learningUtility = FindLearningUtility(item.Id);
            if (learningUtility != null)
                LearningUtilities.Remove(learningUtility);
            else
                throw new InvalidOperationException("Item werd niet in uw verlanglijstje terug gevonden.");
        }

        private LearningUtility FindLearningUtility(int id)
        {
            return LearningUtilities.SingleOrDefault(l => l.Id == id);
        }
        #endregion
    }
}