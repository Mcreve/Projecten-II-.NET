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
        private DbSet<FieldOfStudy> fieldsOfStudy;

        public FieldOfStudyRepository(LeermiddelenContext context)
        {
            this.context = context;
            this.fieldsOfStudy = context.FieldsOfStudy;
        }
        public IQueryable<FieldOfStudy> FindAll()
        {
            return fieldsOfStudy;
        }

        public FieldOfStudy FindBy(int id)
        {
            return fieldsOfStudy.Find(id);
        }
    }
}