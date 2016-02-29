using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;

namespace DidactischeLeermiddelen.Models.Domain
{
    public interface ITargetGroupRepository
    {
        IQueryable<TargetGroup> FindAll();
        TargetGroup FindBy(int id);
    }
}