using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DidactischeLeermiddelen.Models;
using DidactischeLeermiddelen.Models.DAL;
using DidactischeLeermiddelen.Models.Domain;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;
using DidactischeLeermiddelen.Models.Domain.Users;
using Microsoft.Ajax.Utilities;
using System.Text.RegularExpressions;

namespace DidactischeLeermiddelen.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ILearningUtilityDetailsRepository learningUtilityDetailsRepository;

        public CatalogController(ILearningUtilityDetailsRepository learningUtilityDetailsRepository)
        {
            this.learningUtilityDetailsRepository = learningUtilityDetailsRepository;

        }



        // GET: Catalog
        public ActionResult Index(User user)
        {

            IEnumerable<LearningUtilityDetails> catalog = null;

            if (User.Identity.IsAuthenticated)
            {
                catalog = user.GetLearningUtilities(learningUtilityDetailsRepository);
            }
            else
            {
                catalog = learningUtilityDetailsRepository.FindAll().Where(learningUtilityDetails => learningUtilityDetails.Loanable == true);
            }

            IEnumerable<CatalogViewModel> catalogViewModels =
                catalog.Select(learningUtilityDetails => new CatalogViewModel(learningUtilityDetails)).ToList();

            return View(catalogViewModels);
        }

        // GET: Catalog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LearningUtilityDetails learningUtilityDetails = learningUtilityDetailsRepository.FindBy((int)id);
            if (learningUtilityDetails == null)
            {
                return HttpNotFound();
            }
            return View(learningUtilityDetails);
        }

        /// <summary>
        /// Searches for a matching pattern in names and descriptions from the LearningUtilityDetailsRepository
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        /// 
        public ActionResult Search(string Pattern)
        {
            throw new NotImplementedException();
        }
       

    }

}


