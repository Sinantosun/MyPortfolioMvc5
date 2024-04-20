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
    public class ProjectDetailsController : Controller
    {
        ProjectRepository ProjectRepository = new ProjectRepository();
        DbCvEntities _context = new DbCvEntities();
        public ActionResult Index(int id)
        {
            var value = ProjectRepository.Filter(x => x.ProjectID == id);
            return View(value);
        }

        public PartialViewResult GetImagesFilter(int id)
        {
            var value = _context.ProjectImages.Where(x => x.ProjectID == id).ToList();
            return PartialView(value);
        }
       
    }
}