using MyPortfolioAspNetMvc5.Models.Entity;
using MyPortfolioAspNetMvc5.Repositoryies;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolioAspNetMvc5.Controllers
{
    [AllowAnonymous]
    public class ProjectDetailsController : Controller
    {
        ProjectRepository projectRepository = new ProjectRepository();
        ProjectImagesRepository _projectImagesRepository = new ProjectImagesRepository();

        public ActionResult Index(int id)
        {

            return View();
        }

        public PartialViewResult GetImagesFilter(int id)
        {
         
            var value = _projectImagesRepository.FilterList(x => x.ProjectID == id);
            return PartialView(value);
        }

        public PartialViewResult UIProjectDetailHeadPartial()
        {
            return PartialView();
        }
        public PartialViewResult UIProjecDetailPartial(int id)
        {
            var value = projectRepository.Filter(x => x.IsActive == true && x.ProjectID == id);
            return PartialView(value);
        }
    }
}