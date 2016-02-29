using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DidactischeLeermiddelen.Models.Domain;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;

namespace DidactischeLeermiddelen.Models.DAL
{
    public class TargetGroupRepository : ITargetGroupRepository
    {
        private LeermiddelenContext context;
        private DbSet<TargetGroup> targetGroups;

        public TargetGroupRepository(LeermiddelenContext context)
        {
            this.context = context;
            this.targetGroups = context.TargetGroups;
        }
        public IQueryable<TargetGroup> FindAll()
        {
            return targetGroups;
        }

        public TargetGroup FindBy(int id)
        {
            return targetGroups.Find(id);
        }
    }
}