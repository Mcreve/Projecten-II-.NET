using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DidactischeLeermiddelen.Models;
using DidactischeLeermiddelen.Models.Domain;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;
using DidactischeLeermiddelen.Models.Domain.Users;

namespace DidactischeLeermiddelen.Controllers
{
    [Authorize]
    public class CatalogController : Controller
    {
        private readonly ILearningUtilityDetailsRepository learningUtilityDetailsRepository;
        private ITargetGroupRepository targetGroupRepository;
        private IFieldOfStudyRepository fieldOfStudyRepository;

        public CatalogController(ILearningUtilityDetailsRepository learningUtilityDetailsRepository, ITargetGroupRepository targetGroupRepository, IFieldOfStudyRepository fieldOfStudyRepository)
        {
            this.learningUtilityDetailsRepository = learningUtilityDetailsRepository;
            this.targetGroupRepository = targetGroupRepository;
            this.fieldOfStudyRepository = fieldOfStudyRepository;
        }



        // GET: Catalog
        public ActionResult Index(User user,string searchString, int? fieldOfStudy, int? targetGroup)
        {

            IEnumerable<LearningUtilityDetails> catalog = null;
            ViewBag.TargetGroups = GetTargetGroupsSelectList();
            ViewBag.FieldsOfStudy = GetFieldOfStudySelectList();

            if (User.Identity.IsAuthenticated)
            {
                catalog = user.GetLearningUtilities(learningUtilityDetailsRepository).OrderBy(l => l.Name);
            }
            else
            {
                catalog = learningUtilityDetailsRepository.FindAll();
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                catalog = catalog.Where(l => l.Name.Contains(searchString) || l.Description.Contains(searchString));
            }

            if (fieldOfStudy != null)
            {
                catalog = catalog.Where(l => l.FieldsOfStudy.SingleOrDefault(f => f.Id == fieldOfStudy) != null);
            }

            if (targetGroup != null)
            {
                catalog = catalog.Where(l => l.TargetGroups.SingleOrDefault(t => t.Id == targetGroup) != null);
            }
            if (!catalog.Any())
                TempData["info"] = "Geen items gevonden voor opgegeven zoekcriteria.";
            IEnumerable<CatalogViewModel> catalogViewModel =
                catalog.Select(learningUtilityDetails => new CatalogViewModel(learningUtilityDetails)).ToList();
            if (Request.IsAjaxRequest())
                return PartialView("_SearchResultsPartial", catalogViewModel);
            return View(catalogViewModel);
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

        private SelectList GetTargetGroupsSelectList()
        {
            return new SelectList(targetGroupRepository.FindAll().OrderBy(f => f.Name), "Id", "Name");
        }

        private SelectList GetFieldOfStudySelectList()
        {
            return new SelectList(fieldOfStudyRepository.FindAll().OrderBy(f => f.Name), "Id", "Name");
        }

    }

}


