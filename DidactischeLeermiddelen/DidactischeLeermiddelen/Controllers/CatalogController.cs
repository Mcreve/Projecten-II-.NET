using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DidactischeLeermiddelen.Models;
using DidactischeLeermiddelen.Models.Domain;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;
using DidactischeLeermiddelen.Models.Domain.Users;
using Microsoft.Ajax.Utilities;
using PagedList;

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



        /// <summary>
        /// Index method returns the index view when no ajaxRequest was passed, if ajaxRequest is passed returns partialView.
        /// Filters the objects on fieldOfStudy, targetGroup and/or searchString
        /// </summary>
        /// <param name="user">The user object</param>
        /// <param name="currentFilter">Active searchfilter for paging purposes</param>
        /// <param name="searchString">string to search in catalog</param>
        /// <param name="fieldOfStudy">int to filter on field of study</param>
        /// <param name="targetGroup">int to filter on target group</param>
        /// <param name="page">current page</param>
        /// <returns></returns>
        public ActionResult Index(User user, string currentFilter, string searchString, int? fieldOfStudy, int? targetGroup, int? page)
        {

            IEnumerable<LearningUtilityDetails> catalog = null;
            ViewBag.TargetGroups = GetTargetGroupsSelectList();
            ViewBag.FieldsOfStudy = GetFieldOfStudySelectList();
            catalog = learningUtilityDetailsRepository.FindAll().OrderBy(l => l.Name);
            if (user.GetType() == typeof(Student))
            {
                catalog = catalog.Where(l => l.Loanable == true);
            }

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

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
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            
            if (Request.IsAjaxRequest())
                return PartialView("_SearchResultsPartial", catalogViewModel.ToPagedList(pageNumber, pageSize));
            return View(catalogViewModel.ToPagedList(pageNumber, pageSize));
        }

        // GET: Catalog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            LearningUtilityDetails learningUtilityDetails = learningUtilityDetailsRepository.FindBy((int)id);
            if (learningUtilityDetails == null)
            {
                return HttpNotFound();
            }
            return View(learningUtilityDetails);
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


