using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DidactischeLeermiddelen.Models.Domain.Interfaces;
using DidactischeLeermiddelen.Models.Domain.Products;
using DidactischeLeermiddelen.ViewModels;

namespace DidactischeLeermiddelen.Controllers
{
    [Authorize]
    public class CatalogController : Controller
    {
        private IProductGroupRepository productGroupRepository;

        public CatalogController(IProductGroupRepository productGroupRepository)
        {
            this.productGroupRepository = productGroupRepository;
        }
        [AllowAnonymous]
        public ActionResult Index()
        {
            IEnumerable<ProductGroup> productGroups = productGroupRepository.FindAll();
            IEnumerable<ProductGroupViewModel> productGroupViewModels = productGroups.Select(p => new ProductGroupViewModel(p)).ToList();
            return View(productGroupViewModels);
        }
        
        [HttpGet]
        public ActionResult Search()
        {
            return PartialView("_SearchCatalogPartial");
        }
        [HttpPost]
        public ActionResult Search(string query)
        {
            if (query != null)
            {
                try
                {
                    IEnumerable<ProductGroup> foundProductGroups = productGroupRepository.Search(query);
                    var itemsFound = foundProductGroups.Count();
                    IEnumerable<ProductGroupViewModel> productGroupViewModels = foundProductGroups.Select(p => new ProductGroupViewModel(p)).ToList();


                    return PartialView("_SearchResultCatalogPartial", productGroupViewModels);
                }
                catch (Exception e)
                {
                    // handle exception
                }
            }
            return PartialView("Error");
        }
    }
}