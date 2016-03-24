using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DidactischeLeermiddelen.Models;
using DidactischeLeermiddelen.Models.Domain;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;
using DidactischeLeermiddelen.Models.Domain.Users;
using DidactischeLeermiddelen.ViewModels;
using Microsoft.Ajax.Utilities;
using PagedList;

namespace DidactischeLeermiddelen.Controllers
{
    [Authorize]
    public class CatalogController : Controller
    {
        private readonly ILearningUtilityRepository learningUtilityRepository;
        private ITargetGroupRepository targetGroupRepository;
        private IFieldOfStudyRepository fieldOfStudyRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="learningUtilityRepository"></param>
        /// <param name="targetGroupRepository"></param>
        /// <param name="fieldOfStudyRepository"></param>
        public CatalogController(ILearningUtilityRepository learningUtilityRepository, ITargetGroupRepository targetGroupRepository, IFieldOfStudyRepository fieldOfStudyRepository)
        {
            this.learningUtilityRepository = learningUtilityRepository;
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
        public ActionResult Index(User user, string currentFilter, string searchString, int? fieldOfStudy, int? targetGroup, int? page, int? currentFieldOfStudy, int? currentTargetGroup)
        {
            IEnumerable<LearningUtility> catalog = null;
            catalog = learningUtilityRepository.FindAll().OrderBy(l => l.Name);
            var userType = user as Student;
            if (userType != null)
            {
                catalog = catalog.Where(l => l.Loanable == true);
            }

            if (searchString != null || fieldOfStudy != null || targetGroup != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
                fieldOfStudy = currentFieldOfStudy;
                targetGroup = currentTargetGroup;
            }

            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentTargetGroup = targetGroup;
            ViewBag.CurrentFieldOfStudy = fieldOfStudy;

            ViewBag.TargetGroups = GetTargetGroupsSelectList(targetGroup);
            ViewBag.FieldsOfStudy = GetFieldOfStudySelectList(fieldOfStudy);

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
                catalog.Select(learningUtility => new CatalogViewModel(learningUtility)).ToList();
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            
            if (Request.IsAjaxRequest())
                return PartialView("_SearchResultsPartial", catalogViewModel.ToPagedList(pageNumber, pageSize));
            return View(catalogViewModel.ToPagedList(pageNumber, pageSize));
        }

        /// <summary>
        /// Details view of the learning utility, creates the catalogviewmodels and returns it.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="currentFilter"></param>
        /// <param name="currentFieldOfStudy"></param>
        /// <param name="currentTargetGroup"></param>
        /// <returns></returns>
        // GET: Catalog/Details/5
        public ActionResult Details(int? id, int? page, string currentFilter, int? currentFieldOfStudy, int? currentTargetGroup)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            LearningUtility learningUtility = learningUtilityRepository.FindBy((int)id);
            if (learningUtility == null)
            {
                return HttpNotFound();
            }

            ViewBag.CurrentFilter = currentFilter;
            ViewBag.Page = page;
            ViewBag.CurrentFieldOfStudy = currentFieldOfStudy;
            ViewBag.CurrentTargetGroup = currentTargetGroup;
            learningUtility.DateWanted = DateTime.Now;
            LearningUtilityViewModel learningUtilityViewModel = new LearningUtilityViewModel(learningUtility);
            return View(learningUtilityViewModel);
        }

        private SelectList GetTargetGroupsSelectList(int? targetGroup)
        {
            return new SelectList(targetGroupRepository.FindAll().OrderBy(f => f.Name), "Id", "Name", targetGroup != null ? targetGroup.ToString() : "");
        }

        private SelectList GetFieldOfStudySelectList(int? fieldOfStudy)
        {
            return new SelectList(fieldOfStudyRepository.FindAll().OrderBy(f => f.Name), "Id", "Name", fieldOfStudy != null ? fieldOfStudy.ToString() : "");
        }

    }

}


