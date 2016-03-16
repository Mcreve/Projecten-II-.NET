using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;
using System.Text.RegularExpressions;

namespace DidactischeLeermiddelen.Models.Domain
{
    public interface ILearningUtilityRepository
    {
        IQueryable<LearningUtility> FindAll();
        LearningUtility FindBy(int id);
        List<LearningUtility> Search(string query);
        void SaveChanges();

    }
}
