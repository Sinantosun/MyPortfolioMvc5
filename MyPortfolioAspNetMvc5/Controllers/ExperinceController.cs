using MyPortfolioAspNetMvc5.Models.Entity;
using MyPortfolioAspNetMvc5.Repositoryies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolioAspNetMvc5.Controllers
{
    public class ExperinceController : Controller
    {


        ExperincesRepository experincesRepository = new ExperincesRepository();

        public ActionResult Index()
        {
            var values = experincesRepository.GetList();
            return View(values);
        }
        [HttpGet]
        public ActionResult AddExperince()
        {
          
            return View();
        }
        [HttpPost]
        public ActionResult AddExperince(Experinces experinces)
        {

            return View();
        }
    }
}