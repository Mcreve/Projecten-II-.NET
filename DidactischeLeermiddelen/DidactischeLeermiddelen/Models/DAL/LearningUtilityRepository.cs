using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;

using DidactischeLeermiddelen.Models.Domain;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;
using DidactischeLeermiddelen.Models.Domain.Users;
using WebGrease.Css.Extensions;

namespace DidactischeLeermiddelen.Models.DAL
{
    public class LearningUtilityRepository : ILearningUtilityRepository
    {
        private readonly LeermiddelenContext context;
        private readonly DbSet<LearningUtility> learningUtilityList;

        public LearningUtilityRepository(LeermiddelenContext context)
        {
            this.context = context;
            learningUtilityList = context.LearningUtilityList;
        }
        public IQueryable<LearningUtility> FindAll()
        {
            return learningUtilityList;
        }

        public LearningUtility FindBy(int id)
        {
            return learningUtilityList.Find(id);
        }

        public List<LearningUtility> Search(string query)
        {
            List<LearningUtility> resultName =
             learningUtilityList.Where(learningUtility => learningUtility.Name.Contains(query)).ToList();
            return resultName;
        }
        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}