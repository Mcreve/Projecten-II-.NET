using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;

namespace DidactischeLeermiddelen.Models.Domain
{
    public interface ILearningUtilityDetailsRepository
    {
        IQueryable<LearningUtilityDetails> FindAll();
        LearningUtilityDetails FindBy(int id);
        List<LearningUtilityDetails> Search(string query);

    }
}
