using MyPortfolioAspNetMvc5.Repositoryies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolioAspNetMvc5.Controllers
{
    public class AboutsController : Controller
    {

        AboutRepository AboutRepository = new AboutRepository();
        public ActionResult Index()
        {
            var values = AboutRepository.GetList();
            return View(values);
        }
    }
}