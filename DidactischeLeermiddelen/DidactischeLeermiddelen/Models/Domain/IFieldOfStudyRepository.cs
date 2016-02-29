using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;

namespace DidactischeLeermiddelen.Models.Domain
{
    public interface IFieldOfStudyRepository
    {
        IQueryable<FieldOfStudy> FindAll();
        FieldOfStudy FindBy(int id);
    }
}