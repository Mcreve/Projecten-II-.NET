using System;
using System.Linq;
using DidactischeLeermiddelen.Models.Domain;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;

namespace DidactischeLeermiddelen.Models.DAL
{
    public class LearningUtilityDetailsRepository : ILearningUtilityDetailsRepository
    {
        public IQueryable<LearningUtilityDetails> FindAll()
        {
            throw new NotImplementedException();
        }

        public LearningUtilityDetails FindBy(int id)
        {
            throw new NotImplementedException();
        }
    }
}