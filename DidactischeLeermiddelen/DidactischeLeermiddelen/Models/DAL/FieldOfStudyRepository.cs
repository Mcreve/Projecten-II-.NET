using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DidactischeLeermiddelen.Models.Domain;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;

namespace DidactischeLeermiddelen.Models.DAL
{
    public class FieldOfStudyRepository : IFieldOfStudyRepository
    {
        private LeermiddelenContext context;
        private DbSet<FieldOfStudy> fieldsOfStudies;

        public FieldOfStudyRepository(LeermiddelenContext context)
        {
            this.context = context;
            this.fieldsOfStudies = context.FieldsOfStudies;
        }
        public IQueryable<FieldOfStudy> FindAll()
        {
            return fieldsOfStudies;
        }

        public FieldOfStudy FindBy(int id)
        {
            return fieldsOfStudies.Find(id);
        }
    }
}