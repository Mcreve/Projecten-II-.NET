using System;
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
        private readonly DbSet<LearningUtility> learningUtilities;

        public LearningUtilityRepository(LeermiddelenContext context)
        {
            this.context = context;
            learningUtilities = context.LearningUtilities;
        }
        public IQueryable<LearningUtility> FindAll()
        {
            return learningUtilities;
        }

        public LearningUtility FindBy(int id)
        {
            return learningUtilities.Find(id);
        }

        public List<LearningUtility> Search(string query)
        {
            List<LearningUtility> resultName =
             learningUtilities.Where(learningUtility => learningUtility.Name.Contains(query)).ToList();
            return resultName;
        }
        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}